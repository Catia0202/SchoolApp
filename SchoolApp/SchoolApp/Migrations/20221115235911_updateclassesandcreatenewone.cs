using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolApp.Migrations
{
    public partial class updateclassesandcreatenewone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_AspNetUsers_UserId",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_turma_turmaid",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_falta_Aluno_alunoid",
                table: "falta");

            migrationBuilder.DropForeignKey(
                name: "FK_falta_Disciplina_disciplinaid",
                table: "falta");

            migrationBuilder.DropForeignKey(
                name: "FK_Nota_Aluno_alunoId",
                table: "Nota");

            migrationBuilder.DropForeignKey(
                name: "FK_Nota_Disciplina_disciplinaId",
                table: "Nota");

            migrationBuilder.DropForeignKey(
                name: "FK_Nota_turma_turmaId",
                table: "Nota");

            migrationBuilder.DropForeignKey(
                name: "FK_turma_AspNetUsers_UserId",
                table: "turma");

            migrationBuilder.DropTable(
                name: "turmaDisciplina");

            migrationBuilder.DropPrimaryKey(
                name: "PK_turma",
                table: "turma");

            migrationBuilder.DropIndex(
                name: "IX_turma_UserId",
                table: "turma");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nota",
                table: "Nota");

            migrationBuilder.DropPrimaryKey(
                name: "PK_falta",
                table: "falta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Disciplina",
                table: "Disciplina");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Configuracao",
                table: "Configuracao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Aluno",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "turma");

            migrationBuilder.DropColumn(
                name: "Fotourl",
                table: "turma");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "turma");

            migrationBuilder.RenameTable(
                name: "turma",
                newName: "Turmas");

            migrationBuilder.RenameTable(
                name: "Nota",
                newName: "Notas");

            migrationBuilder.RenameTable(
                name: "falta",
                newName: "Faltas");

            migrationBuilder.RenameTable(
                name: "Disciplina",
                newName: "Disciplinas");

            migrationBuilder.RenameTable(
                name: "Configuracao",
                newName: "Configuracoes");

            migrationBuilder.RenameTable(
                name: "Aluno",
                newName: "Alunos");

            migrationBuilder.RenameColumn(
                name: "Duracao",
                table: "Turmas",
                newName: "CursoId");

            migrationBuilder.RenameIndex(
                name: "IX_Nota_turmaId",
                table: "Notas",
                newName: "IX_Notas_turmaId");

            migrationBuilder.RenameIndex(
                name: "IX_Nota_disciplinaId",
                table: "Notas",
                newName: "IX_Notas_disciplinaId");

            migrationBuilder.RenameIndex(
                name: "IX_Nota_alunoId",
                table: "Notas",
                newName: "IX_Notas_alunoId");

            migrationBuilder.RenameIndex(
                name: "IX_falta_disciplinaid",
                table: "Faltas",
                newName: "IX_Faltas_disciplinaid");

            migrationBuilder.RenameIndex(
                name: "IX_falta_alunoid",
                table: "Faltas",
                newName: "IX_Faltas_alunoid");

            migrationBuilder.RenameIndex(
                name: "IX_Aluno_UserId",
                table: "Alunos",
                newName: "IX_Alunos_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Aluno_turmaid",
                table: "Alunos",
                newName: "IX_Alunos_turmaid");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Turmas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "Turmas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicio",
                table: "Turmas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Turmas",
                table: "Turmas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notas",
                table: "Notas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faltas",
                table: "Faltas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Disciplinas",
                table: "Disciplinas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Configuracoes",
                table: "Configuracoes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alunos",
                table: "Alunos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Duracao = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fotourl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CursoDisciplinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoId = table.Column<int>(type: "int", nullable: false),
                    DisciplinaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoDisciplinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CursoDisciplinas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CursoDisciplinas_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_CursoId",
                table: "Turmas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoDisciplinas_CursoId",
                table: "CursoDisciplinas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoDisciplinas_DisciplinaId",
                table: "CursoDisciplinas",
                column: "DisciplinaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_AspNetUsers_UserId",
                table: "Alunos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Turmas_turmaid",
                table: "Alunos",
                column: "turmaid",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Faltas_Alunos_alunoid",
                table: "Faltas",
                column: "alunoid",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Faltas_Disciplinas_disciplinaid",
                table: "Faltas",
                column: "disciplinaid",
                principalTable: "Disciplinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Alunos_alunoId",
                table: "Notas",
                column: "alunoId",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Disciplinas_disciplinaId",
                table: "Notas",
                column: "disciplinaId",
                principalTable: "Disciplinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notas_Turmas_turmaId",
                table: "Notas",
                column: "turmaId",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Cursos_CursoId",
                table: "Turmas",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_AspNetUsers_UserId",
                table: "Alunos");

            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Turmas_turmaid",
                table: "Alunos");

            migrationBuilder.DropForeignKey(
                name: "FK_Faltas_Alunos_alunoid",
                table: "Faltas");

            migrationBuilder.DropForeignKey(
                name: "FK_Faltas_Disciplinas_disciplinaid",
                table: "Faltas");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Alunos_alunoId",
                table: "Notas");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Disciplinas_disciplinaId",
                table: "Notas");

            migrationBuilder.DropForeignKey(
                name: "FK_Notas_Turmas_turmaId",
                table: "Notas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Cursos_CursoId",
                table: "Turmas");

            migrationBuilder.DropTable(
                name: "CursoDisciplinas");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Turmas",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Turmas_CursoId",
                table: "Turmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notas",
                table: "Notas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faltas",
                table: "Faltas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Disciplinas",
                table: "Disciplinas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Configuracoes",
                table: "Configuracoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Alunos",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "DataInicio",
                table: "Turmas");

            migrationBuilder.RenameTable(
                name: "Turmas",
                newName: "turma");

            migrationBuilder.RenameTable(
                name: "Notas",
                newName: "Nota");

            migrationBuilder.RenameTable(
                name: "Faltas",
                newName: "falta");

            migrationBuilder.RenameTable(
                name: "Disciplinas",
                newName: "Disciplina");

            migrationBuilder.RenameTable(
                name: "Configuracoes",
                newName: "Configuracao");

            migrationBuilder.RenameTable(
                name: "Alunos",
                newName: "Aluno");

            migrationBuilder.RenameColumn(
                name: "CursoId",
                table: "turma",
                newName: "Duracao");

            migrationBuilder.RenameIndex(
                name: "IX_Notas_turmaId",
                table: "Nota",
                newName: "IX_Nota_turmaId");

            migrationBuilder.RenameIndex(
                name: "IX_Notas_disciplinaId",
                table: "Nota",
                newName: "IX_Nota_disciplinaId");

            migrationBuilder.RenameIndex(
                name: "IX_Notas_alunoId",
                table: "Nota",
                newName: "IX_Nota_alunoId");

            migrationBuilder.RenameIndex(
                name: "IX_Faltas_disciplinaid",
                table: "falta",
                newName: "IX_falta_disciplinaid");

            migrationBuilder.RenameIndex(
                name: "IX_Faltas_alunoid",
                table: "falta",
                newName: "IX_falta_alunoid");

            migrationBuilder.RenameIndex(
                name: "IX_Alunos_UserId",
                table: "Aluno",
                newName: "IX_Aluno_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Alunos_turmaid",
                table: "Aluno",
                newName: "IX_Aluno_turmaid");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "turma",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "turma",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fotourl",
                table: "turma",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "turma",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_turma",
                table: "turma",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nota",
                table: "Nota",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_falta",
                table: "falta",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Disciplina",
                table: "Disciplina",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Configuracao",
                table: "Configuracao",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Aluno",
                table: "Aluno",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "turmaDisciplina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisciplinaId = table.Column<int>(type: "int", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turmaDisciplina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_turmaDisciplina_Disciplina_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_turmaDisciplina_turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_turma_UserId",
                table: "turma",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_turmaDisciplina_DisciplinaId",
                table: "turmaDisciplina",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_turmaDisciplina_TurmaId",
                table: "turmaDisciplina",
                column: "TurmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_AspNetUsers_UserId",
                table: "Aluno",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_turma_turmaid",
                table: "Aluno",
                column: "turmaid",
                principalTable: "turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_falta_Aluno_alunoid",
                table: "falta",
                column: "alunoid",
                principalTable: "Aluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_falta_Disciplina_disciplinaid",
                table: "falta",
                column: "disciplinaid",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Nota_Aluno_alunoId",
                table: "Nota",
                column: "alunoId",
                principalTable: "Aluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Nota_Disciplina_disciplinaId",
                table: "Nota",
                column: "disciplinaId",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Nota_turma_turmaId",
                table: "Nota",
                column: "turmaId",
                principalTable: "turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_turma_AspNetUsers_UserId",
                table: "turma",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
