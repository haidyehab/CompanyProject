using AutoMapper;
using Comapny.DAL.Models;
using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.PL.Helpers;
using Company.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
              employees =await _unitOfWork.EmployeeRepository.GetAll();
             
            else
            
             employees = _unitOfWork.EmployeeRepository.SearchEmployeesByName(SearchValue);

            var mappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmployees);
        }

        //create
        public IActionResult Create()
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll().Result;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVm)
        {
            if (ModelState.IsValid) //server side validation
            {
              employeeVm.ImageName  =DocumentSettings.UploadFile(employeeVm.Image , "Images");
              var MappedEmp = _mapper.Map<EmployeeViewModel,Employee>(employeeVm);
               await  _unitOfWork.EmployeeRepository.Add(MappedEmp);
                await _unitOfWork.complete();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVm);
        }
        //Details
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee =await _unitOfWork.EmployeeRepository.Get(id.Value);
            if (employee is null)
                return NotFound();
            var MappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(ViewName, MappedEmp);

        }

        //Edit

        public async Task<IActionResult> Edit(int? id)
        {
          
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVm)
        {
            if (id != employeeVm.Id)
                return BadRequest();
           
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                    _unitOfWork.EmployeeRepository.Update(MappedEmp);
                   await _unitOfWork.complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVm);
        }

        //Delet

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeVm)
        {
            if (id != employeeVm.Id)
                return BadRequest();
            try
            {
                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                _unitOfWork.EmployeeRepository.Delete(MappedEmp);
               int Result=await  _unitOfWork.complete();
                if (Result > 0)
                    DocumentSettings.DeleteFile(employeeVm.ImageName , "Images");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVm);
            }
        }
         



    }
}
