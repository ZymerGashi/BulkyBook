using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {

            CoverType coverType = new CoverType();

            if (id == null)
            {
                return View(coverType);
            }

            coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());

            if (coverType == null)
            {
                return NotFound();
            
            }

            return View(coverType);
        }



        #region

        [HttpPost]
        public IActionResult Upsert(CoverType coverType)
        {

            if (coverType.ID == 0)
            {
                _unitOfWork.CoverType.Add(coverType);
            }
            else 
            {
                CoverType cvrType = _unitOfWork.CoverType.Get(coverType.ID);
                cvrType.Name = coverType.Name;
                _unitOfWork.CoverType.Update(cvrType);
                
            }
            _unitOfWork.Save();
            return View(nameof(Index));
        }






        [HttpGet]
        public IActionResult GetAll()
        {

            var coverTypes = _unitOfWork.CoverType.GetAll();

            return Json(new { data=coverTypes});
        
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            CoverType coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());

            if (coverType==null)
            {

                return Json(new { success = false, message = "Error while deleting!" });
            
            }

            _unitOfWork.CoverType.Remove(coverType);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Cover type has been deleted successfully!" });

        }


        #endregion




    }
}
