﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using SchoolApp.Data;
using SchoolApp.Data.Entities;
using SchoolApp.Helpers;
using SchoolApp.Models;

namespace SchoolApp.Controllers
{
    public class AlunosController : Controller
    {
      
        private readonly IAlunosRepository _alunosRepository;
        private readonly IImageHelper _imagehelper;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;

        public AlunosController(IAlunosRepository alunosRepository, IImageHelper imagehelper, IUserHelper userHelper,  IConverterHelper converterHelper)
        {
        
            _alunosRepository = alunosRepository;
            _imagehelper = imagehelper;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
        }

        // GET: Alunos
        public IActionResult Index()
        {
            return View(_alunosRepository.GetAll().OrderBy(P => P.Nome));
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _alunosRepository.GetByIdAsync(id.Value);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // GET: Alunos/Create
        [Authorize(Roles ="Admin")] //para varios role's fica  [Authorize(Roles ="Admin,Customer,SuperUser")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alunos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlunoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    path = await _imagehelper.UploadImageAsync(model.ImageFile, "alunos");
                }


                var aluno = _converterHelper.ToAluno(model, path, true);

                //TODO:Modificar para o user que tiver logado
                aluno.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await _alunosRepository.CreateAsync(aluno);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        private Aluno toAluno(AlunoViewModel model, string path)
        {
            return new Aluno
            {
                Id = model.Id,
                ImageUrl= path ,
                Nome = model.Nome,
                Data_Nascimento = model.Data_Nascimento,
                Email = model.Email,
                Genero = model.Genero,
                Morada = model.Morada,
                Telemovel = model.Telemovel,
                User = model.User

            };
        }
        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _alunosRepository.GetByIdAsync(id.Value);
            if (aluno == null)
            {
                return NotFound();
            }
            var model = _converterHelper.ToAlunoViewModel(aluno);
            return View(model);
        }
        private AlunoViewModel ToAlunoViewModel(Aluno aluno)
        {
            return new AlunoViewModel
            {
                Id = aluno.Id,
                ImageUrl = aluno.ImageUrl,
                Nome = aluno.Nome,
                Data_Nascimento = aluno.Data_Nascimento,
                Email = aluno.Email,
                Genero = aluno.Genero,
                Morada = aluno.Morada,
                Telemovel = aluno.Telemovel,
                User = aluno.User
            };
        }
        // POST: Alunos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AlunoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var path = model.ImageUrl;
                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        path = await _imagehelper.UploadImageAsync(model.ImageFile, "alunos");

                    }
                    var aluno = _converterHelper.ToAluno(model, path, false);
                    //TODO:Modificar para o user que tiver logado
                    aluno.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await _alunosRepository.UpdateAsync(aluno);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _alunosRepository.ExistAsync(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Alunos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _alunosRepository.GetByIdAsync(id.Value);
            if (aluno == null)
            {
                return NotFound();
                          }
                return View(aluno);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _alunosRepository.GetByIdAsync(id);
            await _alunosRepository.DeleteAsync(aluno);

            return RedirectToAction(nameof(Index));
        }

        //private bool AlunoExists(int id)
        //{
        //    return _context.Aluno.Any(e => e.Id == id);
        //}
    }
}