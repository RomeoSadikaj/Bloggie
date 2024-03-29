﻿using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            ValidateAddTagRequest(addTagRequest);
            if (ModelState.IsValid)
            {
                //Mapping addTagRequest to tag domain model
                var tag = new Tag
                {
                    Name = addTagRequest.Name,
                    DisplayName = addTagRequest.DisplayName
                };

                await tagRepository.AddAsync(tag);

                return RedirectToAction("List");
            }

            return View();
            
        }


        [HttpGet]
        [ActionName("List")]

        public async Task<IActionResult> List() 
        {
            //use dbcontext to read the tags
            var tags = await tagRepository.GetAllAsync();

            return View(tags);
        }


        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            //1st method
            //var tag = bloggieDbContext.Tags.Find(id);
            
            //2nd method
            //var tag = bloggieDbContext.Tags.SingleOrDefault(t => t.Id == id);
            var tag = await tagRepository.GetAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id          = tag.Id,
                    Name        = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            //Mapping addTagRequest to tag domain model
            var tag = new Tag
            {
                Id          = editTagRequest.Id,
                Name        = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);
            if (updatedTag != null)
            {
                //show success notification
            }    
            else
            {
                //show error notification 
            }
            return RedirectToAction("Edit",new {id = editTagRequest.Id});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if (deletedTag != null)
            {
                //show success notification

            } 

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }


        private void ValidateAddTagRequest(AddTagRequest request)
        {
            if (request.Name != null && request.DisplayName != null)
            {
                if (request.Name == request.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "Name can't be the same as the DisplayName");
                }
            }
        }
    }
}
