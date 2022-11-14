﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class TurmasController : Controller
    {
        private readonly ITurmasRepository _turmasRepository;
        private readonly IImageHelper _imagehelper;
        private readonly IUserHelper _userHelper;

        public TurmasController(ITurmasRepository turmasRepository, IImageHelper imagehelper,IUserHelper userHelper)
        {
            _turmasRepository = turmasRepository;
            _imagehelper = imagehelper;
            _userHelper = userHelper;
        }

        // GET: Turmas
        public IActionResult Index()
        {
            return View(_turmasRepository.GetAll().OrderBy(P => P.Nome));
        }

        // GET: Turmas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _turmasRepository.GetByIdAsync(id.Value);
            if (turma == null)
            {
                return NotFound();
            }

            return View(turma);
        }

        // GET: Turmas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Turmas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TurmaViewModel model)
        {
            var path = string.Empty;

           
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                path = await _imagehelper.UploadImageAsync(model.ImageFile, "turmas");
            }
            if (ModelState.IsValid)
            {
                var turma = new Turma
                {
                    Id = model.Id,
                    Descricao = model.Descricao,
                    Fotourl = path,
                    Duracao = model.Duracao,
                    Nome = model.Nome

                };

                await _turmasRepository.CreateAsync(turma);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

       

        // GET: Turmas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _turmasRepository.GetByIdAsync(id.Value);
            if (turma == null)
            {
                return NotFound();
            }
            return View(turma);
        }

        // POST: Turmas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Duracao,Fotourl,Descricao")] Turma turma)
        {
            if (id != turma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _turmasRepository.UpdateAsync(turma);

                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }

            return View(turma);
        }

        // GET: Turmas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _turmasRepository.GetByIdAsync(id.Value);
            if (turma == null)
            {
                return NotFound();
            }
            return View(turma);
        }

        // POST: Turmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turma = await _turmasRepository.GetByIdAsync(id);
            await _turmasRepository.DeleteAsync(turma);

            return RedirectToAction(nameof(Index));
        }

        //private bool TurmaExists(int id)
        //{
        //    return _context.turma.Any(e => e.Id == id);
        //}
    }
}
