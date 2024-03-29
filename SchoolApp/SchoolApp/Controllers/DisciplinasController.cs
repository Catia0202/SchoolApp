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

namespace SchoolApp.Controllers
{
    public class DisciplinasController : Controller
    {
        private readonly IDisciplinasRepository _disciplinasRepository;
        private readonly IUserHelper _userHelper;

        public DisciplinasController(IDisciplinasRepository disciplinasRepository, IUserHelper userHelper)
        {
            _disciplinasRepository = disciplinasRepository;
            _userHelper = userHelper;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_disciplinasRepository.GetAll().OrderBy(P => P.Nome));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var disciplina = await _disciplinasRepository.GetByIdAsync(id.Value);
            if (disciplina == null)
            {
                return View("Error");
            }

            return View(disciplina);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Disciplinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descrição,Duracao")] Disciplina disciplina)
        {
            if (ModelState.IsValid)
            {

                await _disciplinasRepository.CreateAsync(disciplina);
                return RedirectToAction(nameof(Index));
            }
            return View(disciplina);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            var turma = await _disciplinasRepository.GetByIdAsync(id.Value);
            if (turma == null)
            {
                return View("Error");
            }
            return View(turma);
        }

        // POST: Disciplinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descrição,Duracao")] Disciplina disciplina)
        {
            if (id != disciplina.Id)
            {
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _disciplinasRepository.UpdateAsync(disciplina);

                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }

            return View(disciplina);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var disciplina = await _disciplinasRepository.GetByIdAsync(id.Value);
            if (disciplina == null)
            {
                return View("Error");
            }
            return View(disciplina);
        }

        // POST: Disciplinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplina = await _disciplinasRepository.GetByIdAsync(id);

            try
            {
                await _disciplinasRepository.DeleteAsync(disciplina);
                ViewBag.errormessage = "Disciplina deletada com sucesso!";
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.TituloErro = "Erro ao apagar este disciplina";
                    ViewBag.MensagemErro = "Não pode apagar a disciplina pois a mesma está a ser utilizada";
                    return View("Error");
                }

                return View();
            }

          

            return RedirectToAction(nameof(Index));
        }

   




        //private bool DisciplinaExists(int id)
        //{
        //    return _context.Disciplina.Any(e => e.Id == id);
        //}
    }
}
