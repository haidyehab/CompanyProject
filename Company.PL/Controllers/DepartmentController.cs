using AutoMapper;
using Comapny.DAL.Models;
using Company.BLL.Interfaces;
using Company.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
         
        
        //department/index
        public async Task<IActionResult> Index()
        {
            var departments =await _unitOfWork.DepartmentRepository.GetAll();
            var MappedDepts = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(MappedDepts);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVm)
        {
            if(ModelState.IsValid) //server side validation
            {
                var MappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVm);
              await  _unitOfWork.DepartmentRepository.Add(MappedDept);
              await  _unitOfWork.complete();
                return RedirectToAction(nameof(Index));
            }
            return View(departmentVm);
        }
        //department/details/1
        //department/details
        public async Task<IActionResult> Details(int? id,string ViewName= "Details")
        {
            if (id is null)
                return BadRequest();
            var department =await  _unitOfWork.DepartmentRepository.Get(id.Value);
            if (department is null)
                return NotFound();
            var mappedDept = _mapper.Map<Department, DepartmentViewModel>(department);
            return View(ViewName, mappedDept);

        }

    public async Task<IActionResult> Edit(int? id)
    {
            return await Details(id , "Edit");
     }

    [HttpPost]
     [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, DepartmentViewModel departmentVm)
    {
            if(id != departmentVm.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVm);
                    _unitOfWork.DepartmentRepository.Update(MappedDept);
                   await _unitOfWork.complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(departmentVm);
    }


  //Delete
       public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id,"Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete([FromRoute]int id, DepartmentViewModel departmentVm)
     {
            if (id != departmentVm.Id)
                return BadRequest();
            try
            {
                var MappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVm);
                _unitOfWork.DepartmentRepository.Delete(MappedDept);
               await _unitOfWork.complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(departmentVm);
            }
     }


    }
}
