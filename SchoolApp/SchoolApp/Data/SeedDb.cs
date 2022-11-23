using Microsoft.AspNetCore.Identity;
using SchoolApp.Data;
using SchoolApp.Data.Entities;
using SchoolApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IConfiguracaoRepository _configuracaoRepository;
        private readonly ICursoRepository _cursoRepository;
        private readonly IDisciplinasRepository _disciplinasRepository;
        private readonly ICursoDisciplinarRepository _cursoDisciplinarRepository;
        private readonly ITurmasRepository _turmasRepository;
        private readonly IAlunosRepository _alunosRepository;
        private readonly INotaRepository _notaRepository;
        private readonly IFaltaRepository _faltaRepository;
        private readonly IAlertaRepository _alertaRepository;
        private Random _random;
        public SeedDb(DataContext context, IUserHelper userHelper,
            IConfiguracaoRepository configuracaoRepository,
            ICursoRepository cursoRepository,
            IDisciplinasRepository disciplinasRepository,
            ICursoDisciplinarRepository cursoDisciplinarRepository,
            ITurmasRepository turmasRepository,
            IAlunosRepository alunosRepository,
            INotaRepository notaRepository,
            IFaltaRepository faltaRepository,
            IAlertaRepository alertaRepository)
        {
            _context = context;
            _userHelper = userHelper;
            _configuracaoRepository = configuracaoRepository;
            _cursoRepository = cursoRepository;
            _disciplinasRepository = disciplinasRepository;
            _cursoDisciplinarRepository = cursoDisciplinarRepository;
            _turmasRepository = turmasRepository;
            _alunosRepository = alunosRepository;
            _notaRepository = notaRepository;
            _faltaRepository = faltaRepository;
            _alertaRepository = alertaRepository;
            _random = new Random();
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Funcionario");
            await _userHelper.CheckRoleAsync("Aluno");

            var user = await _userHelper.GetUserByEmailAsync("admin@gmail.com");
            if(user == null)
            {
                user = new User
                {
                    FirstName = "catias",
                    LastName = "santos",
                    Email = "admin@gmail.com",
                    UserName="admin@gmail.com",
                    Password="123456",
                    passwordchanged=true
                

                };
                
                var result = await _userHelper.AddUserAsync(user, "123456");
                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }
            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }
            await AddFuncionario();
            await AddAdminDefaultConfig();
            await AddCursos();
            await AddDisciplinas();
            await AddCursoDisciplinas();
            await AddTurmas();
            await AddAlunos();
            
           

        }

      

        private async Task AddAlunos()
        {
            //----------Aluno1--------------------
            Aluno aluno1 = new Aluno
            {
                PrimeiroNome = "Pedro",
                UltimoNome = "Silva",
                Email = "pedrosilva@gmail.com",
                Data_Nascimento = "11/02/2001",
                Genero = "Masculino",
                Morada = "Rua Reynaldo ,2600-128,Sao João Dos Montes, Lisboa",
                Telemovel = 915438293,
                turmaid =1 ,
                ImageUrl = "~/images/alunos/aluno1.jpg"

            };
          
            var user1 = await _userHelper.GetUserByEmailAsync(aluno1.Email);
            if (user1 == null)
            {
                user1 = new User
                {

                    FirstName = aluno1.PrimeiroNome,
                    LastName = aluno1.UltimoNome,
                    Email = aluno1.Email,
                    UserName = aluno1.Email,
                    Password = aluno1.PrimeiroNome + "123456",
                    passwordchanged = true

                };
                
                
                
                await _userHelper.AddUserAsync(user1, user1.Password);
                await _userHelper.AddUserToRoleAsync(user1, "Aluno");
                aluno1.User = user1;
                await _alunosRepository.CreateAsync(aluno1);
            }

            //-----------------------------------------------------------

            //----------Aluno2--------------------
            Aluno aluno2 = new Aluno
            {
                PrimeiroNome = "Silvia",
                UltimoNome = "Pereira",
                Email = "SilviaPereira@gmail.com",
                Data_Nascimento = "02/08/2000",
                Genero = "Feminino",
                Morada = "Rua Pedro Gonçalves ,2600-194,Benavente, Lisboa",
                Telemovel = 915123652,
                turmaid = 1,
                ImageUrl = "~/images/alunos/aluno2.jpg"

            };
           
            var user2 = await _userHelper.GetUserByEmailAsync(aluno2.Email);
            if (user2 == null)
            {
                user2 = new User
                {

                    FirstName = aluno2.PrimeiroNome,
                    LastName = aluno2.UltimoNome,
                    Email = aluno2.Email,
                    UserName = aluno2.Email,
                    Password = aluno2.PrimeiroNome + "123456",
                    passwordchanged = true

                };
              
                await _userHelper.AddUserAsync(user2, user2.Password);
                await _userHelper.AddUserToRoleAsync(user2, "Aluno");
                aluno2.User = user2;
                await _alunosRepository.CreateAsync(aluno2);
            }

            //-----------------------------------------------------------
            //----------Aluno3--------------------
            Aluno aluno3 = new Aluno
            {
                PrimeiroNome = "João",
                UltimoNome = "Alexandre",
                Email = "JoaoAlexandre@gmail.com",
                Data_Nascimento = "05/04/1987",
                Genero = "Masculino",
                Morada = "Rua Marchal ,2490-123,Peniche",
                Telemovel = 912323687,
                turmaid = 2,
                ImageUrl = "~/images/alunos/aluno3.jpg"

            };
          
            var user3 = await _userHelper.GetUserByEmailAsync(aluno3.Email);
            if (user3== null)
            {
                user3 = new User
                {

                    FirstName = aluno3.PrimeiroNome,
                    LastName = aluno3.UltimoNome,
                    Email = aluno3.Email,
                    UserName = aluno3.Email,
                    Password = aluno3.PrimeiroNome + "123456",
                    passwordchanged = true

                };
              
                await _userHelper.AddUserAsync(user3, user3.Password);
                await _userHelper.AddUserToRoleAsync(user3, "Aluno");
                aluno3.User = user3;
                await _alunosRepository.CreateAsync(aluno3);
            }

     
        }

        private async Task AddTurmas()
        {
            var existe = _context.Turmas.FirstOrDefault() == null ? true : false;
            if (existe)
            {
                Turma turma1 = new Turma
                {
                    Nome = "Turma de Robótica",
                    CursoId = 1,
                    DataInicio = DateTime.Today.AddDays(-80),
                    DataFim = DateTime.Today.AddDays(200)


                };

                Turma turma2 = new Turma
                {
                    Nome = "Turma de Robótica 2",
                    CursoId = 1,
                    DataInicio = DateTime.Today.AddDays(-70),
                    DataFim = DateTime.Today.AddDays(240)


                };
                Turma turma3 = new Turma
                {
                    Nome = "Turma de Multimédia",
                    CursoId = 2,
                    DataInicio = DateTime.Today.AddDays(-5),
                    DataFim = DateTime.Today.AddDays(180)


                };
                Turma turma4 = new Turma
                {
                    Nome = "Turma de Marketing Digital",
                    CursoId = 3,
                    DataInicio = DateTime.Today.AddDays(-10),
                    DataFim = DateTime.Today.AddDays(120)


                };

                await _turmasRepository.CreateAsync(turma1);
                await _turmasRepository.CreateAsync(turma2);
                await _turmasRepository.CreateAsync(turma3);
                await _turmasRepository.CreateAsync(turma4);
            }
        }

        private async Task AddCursoDisciplinas()
        {
            var existe = _context.CursoDisciplinas.FirstOrDefault() == null ? true : false;
            if (existe)
            {
                //disciplinas para curso robotica
                CursoDisciplina cursoDisciplina1 = new CursoDisciplina
                {
                    CursoId = 1,
                    DisciplinaId = 1
                };
                CursoDisciplina cursoDisciplina2 = new CursoDisciplina
                {
                    CursoId = 1,
                    DisciplinaId = 2
                };
                CursoDisciplina cursoDisciplina3 = new CursoDisciplina
                {
                    CursoId = 1,
                    DisciplinaId = 5
                };
                //disciplinas para curso multimedia
                CursoDisciplina cursoDisciplina4 = new CursoDisciplina
                {
                    CursoId = 2,
                    DisciplinaId = 3
                };
                CursoDisciplina cursoDisciplina5 = new CursoDisciplina
                {
                    CursoId = 2,
                    DisciplinaId = 5
                };
                CursoDisciplina cursoDisciplina6 = new CursoDisciplina
                {
                    CursoId = 2,
                    DisciplinaId = 6
                };
                //disciplinas para curso marketing
                CursoDisciplina cursoDisciplina7 = new CursoDisciplina
                {
                    CursoId = 3,
                    DisciplinaId = 4
                };
                CursoDisciplina cursoDisciplina8 = new CursoDisciplina
                {
                    CursoId = 3,
                    DisciplinaId = 5
                };
                CursoDisciplina cursoDisciplina9 = new CursoDisciplina
                {
                    CursoId = 3,
                    DisciplinaId = 6
                };

                await _cursoDisciplinarRepository.CreateAsync(cursoDisciplina1);
                await _cursoDisciplinarRepository.CreateAsync(cursoDisciplina2);
                await _cursoDisciplinarRepository.CreateAsync(cursoDisciplina3);
                await _cursoDisciplinarRepository.CreateAsync(cursoDisciplina4);
                await _cursoDisciplinarRepository.CreateAsync(cursoDisciplina5);
                await _cursoDisciplinarRepository.CreateAsync(cursoDisciplina6);
                await _cursoDisciplinarRepository.CreateAsync(cursoDisciplina7);
                await _cursoDisciplinarRepository.CreateAsync(cursoDisciplina8);
                await _cursoDisciplinarRepository.CreateAsync(cursoDisciplina9);

            }
        }

        private async Task AddDisciplinas()
        {
            var existe = _context.Disciplinas.FirstOrDefault() == null ? true : false;
            if (existe)
            {
                Disciplina disciplina1 = new Disciplina()  //Especial Para Róbotica1
                {
                    Nome = "Programação guiada através de scripts",
                    Descrição = "Linguagem de script ou scripting é uma linguagem de programação que suporta scripts, programas escritos para um sistema de tempo de execução especial.",
                    Duracao = 20
                };
                Disciplina disciplina2 = new Disciplina()  //Especial Para Róbotica2
                {
                    Nome = "Introdução de robôs colaborativos.",
                    Descrição = "A Robótica Colaborativa define-se pela interação entre Homem e Robô dividindo uma tarefa em um mesmo espaço de trabalho",
                    Duracao = 20
                };
                Disciplina disciplina3 = new Disciplina()  //Especial Para Multimédia3
                {
                    Nome = "Compreender a origem da comunicação e os diferentes tipos de media",
                    Descrição = "Meios de comunicação são ferramentas que possibilitam a comunicação entre os indivíduos, propiciando a difusão de informações. ",
                    Duracao = 10
                };
                Disciplina disciplina4 = new Disciplina()  //Especial Para Marketing3
                {
                    Nome = "Introdução ao Marketing e ao Marketing Digital",
                    Descrição = "Marketing digital são ações de comunicação que as empresas podem utilizar por meio da internet",
                    Duracao = 20
                };
                Disciplina disciplina5 = new Disciplina()  //Disciplina Comum
                {
                    Nome = "Inglês",
                    Descrição = "Aprender uma nova língua é importante, muito mais quando essa mesma língua é bastante utilizada no nosso dia a dia",
                    Duracao = 40
                };
                Disciplina disciplina6 = new Disciplina()  //Disciplina Comum
                {
                    Nome = "Matemática",
                    Descrição = "Saber matemática é crucial para o dia a dia",
                    Duracao = 40
                };

                await _disciplinasRepository.CreateAsync(disciplina1);
                await _disciplinasRepository.CreateAsync(disciplina2);
                await _disciplinasRepository.CreateAsync(disciplina3);
                await _disciplinasRepository.CreateAsync(disciplina4);
                await _disciplinasRepository.CreateAsync(disciplina5);
                await _disciplinasRepository.CreateAsync(disciplina6);
            }
        }

        private async Task AddCursos()
        {
            var existe = _context.Cursos.FirstOrDefault() == null ? true : false;
            if (existe)
            {
                Curso curso1 = new Curso()
                {
                    Nome = "Curso de Robótica",
                    Descricao = "Falar em robótica transporta-nos às vezes para o futuro. Mas a verdade é que a robótica é uma área atual, está a acontecer e a desenvolver-se agora! A procura por profissionais qualificados na área continua a crescer. As empresas querem encontrar os melhores especialistas para programar e reprogramar os robôs sem contactar o fornecedor ou o fabricante.",
                    Duracao = 700,
                    Fotourl = "~/images/cursos/cursoderobotica.jpg"
                };
                Curso curso2 = new Curso()
                {
                    Nome = "Curso de Multimédia",
                    Descricao = "Trabalhar em Multimédia tem uma grande vantagem. A possibilidade de complementares os teus conhecimentos nesta área com outra atividade! Por exemplo, podes conciliar multimédia com programação, publicidade ou com o marketing digital. Podes escrever, pensar, desenhar, montar e gerir. Tudo para o mesmo projeto!",
                    Duracao = 650,
                    Fotourl = "~/images/cursos/multimedia.jpg"
                };

                Curso curso3 = new Curso()
                {
                    Nome = "Curso de Marketing Digital",
                    Descricao = "Sabias que o marketing digital está a expandir-se a uma taxa de aproximadamente 10 vezes mais do que outros setores da área mais tradicionais?Isso significa que a procura por profissionais da área do marketing digital está a superar a oferta atual.Porquê? Uma grande parte das pequenas e médias empresas não têm profissionais especializados em pensar estrategicamente a presença online e notoriedade das marcas no digital.",
                    Duracao = 650,
                    Fotourl = "~/images/cursos/marketing.jpg"
                };

                await _cursoRepository.CreateAsync(curso1);
                await _cursoRepository.CreateAsync(curso2);
                await _cursoRepository.CreateAsync(curso3);
            }
           
        }

        private async Task AddFuncionario()
        {
            var user = await _userHelper.GetUserByEmailAsync("funcionario@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Rui",
                    LastName = "Santos",
                    Email = "funcionario@gmail.com",
                    UserName = "funcionario@gmail.com",
                    Password = "123456",
                    passwordchanged = true


                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
                await _userHelper.AddUserToRoleAsync(user, "Funcionario");
            }
            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Funcionario");
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Funcionario");
            }
        }

        public async Task AddAdminDefaultConfig()
        {
            Configuracao configuracao = new Configuracao
            {
                MaximoAlunosNaTurma = 20,
                MaximoDisciplinasPorTurma = 30,
                PercentagemdeFaltas = 10
            };
            await _configuracaoRepository.CreateAsync(configuracao);
        }

    }
}

