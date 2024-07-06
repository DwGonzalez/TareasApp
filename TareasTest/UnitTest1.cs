using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using TareasApp.Controllers;
using TareasApp.Data;
using TareasApp.Entities;
using TareasApp.Repository;

namespace TareasTest
{
    public class UnitTest1
    {
        private List<Tarea> tareas = new List<Tarea>
        {
            new Tarea { Id = 1, Title = "Task 1", Description = "Description for Task 1", IsCompleted = false, UsuarioId = "user1" },
            new Tarea { Id = 2, Title = "Task 2", Description = "Description for Task 2", IsCompleted = true, UsuarioId = "user2" },
            new Tarea { Id = 3, Title = "Task 3", Description = "Description for Task 3", IsCompleted = false, UsuarioId = "user3" },
            new Tarea { Id = 4, Title = "Task 4", Description = "Description for Task 4", IsCompleted = true, UsuarioId = "user4" }
        };


        [Fact]
        public async Task Get_AllTareas_Ok()
        {
            // arrange
            Mock<ITareasRepository> _tareasRepositoryMock = new Mock<ITareasRepository>();
            _tareasRepositoryMock
                .Setup(x => x.GetAllTareasAsync())
                 .ReturnsAsync(tareas);

            // act
            var result = await _tareasRepositoryMock.Object.GetAllTareasAsync();

            // assert
            int expectedCOunt = tareas.Count;

            Assert.Equal(expectedCOunt, result.Count);
        }

        [Fact]
        public async Task Get_AllTareas_Empty()
        {
            // arrange 
            Mock<ITareasRepository> _tareasRepositoryMock = new Mock<ITareasRepository>();
            _tareasRepositoryMock
                .Setup(x => x.GetAllTareasAsync())
                 .ReturnsAsync(new List<Tarea>());

            // act
            var result = await _tareasRepositoryMock.Object.GetAllTareasAsync();

            // assert
            int expectedCOunt = 0;

            Assert.Equal(expectedCOunt, result.Count);
        }

        [Fact]
        public async Task Get_AllTareasByUser_OK()
        {
            // arrange
            string usuarioId = "user1";
            var expectedTareas = tareas.Where(x => x.UsuarioId == usuarioId).ToList();

            Mock<ITareasRepository> _tareasRepositoryMock = new Mock<ITareasRepository>();
            _tareasRepositoryMock
                .Setup(x => x.GetAllUserTareas(usuarioId))
                 .ReturnsAsync(expectedTareas);

            // act
            var result = await _tareasRepositoryMock.Object.GetAllUserTareas(usuarioId);

            // assert
            Assert.NotNull(result);
            Assert.Equal(expectedTareas.Count, result.Count);
            Assert.Equal(expectedTareas, result);
        }

        [Fact]
        public async Task Get_AllTareasByUser_Empty()
        {
            // arrange
            string usuarioId = "user0";

            Mock<ITareasRepository> _tareasRepositoryMock = new Mock<ITareasRepository>();
            _tareasRepositoryMock
                .Setup(x => x.GetAllUserTareas(usuarioId))
                 .ReturnsAsync(new List<Tarea>());

            // act
            var result = await _tareasRepositoryMock.Object.GetAllUserTareas(usuarioId);

            // assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Get_TareaById_Ok()
        {
            // arrange
            int tareaId = 1;
            var expectedTarea = new Tarea { Id = tareaId, Title = "Test Tarea" };


            Mock<ITareasRepository> _tareasRepositoryMock = new Mock<ITareasRepository>();
            _tareasRepositoryMock
                .Setup(x => x.GetTareaById(tareaId))
                .ReturnsAsync(expectedTarea);

            // act
            var result = await _tareasRepositoryMock.Object.GetTareaById(tareaId);

            // assert
            Assert.NotNull(result);
            Assert.Equal(expectedTarea.Id, result.Id);
            Assert.Equal(expectedTarea, result);
        }

        [Fact]
        public async Task Get_TareaById_Empty()
        {
            // arrange
            int tareaId = 0;


            Mock<ITareasRepository> _tareasRepositoryMock = new Mock<ITareasRepository>();
            _tareasRepositoryMock
                .Setup(repo => repo.GetTareaById(tareaId))
                .ReturnsAsync((Tarea)null);

            // act
            var result = await _tareasRepositoryMock.Object.GetTareaById(tareaId);

            // assert
            Assert.Null(result);
        }
    }
}