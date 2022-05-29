using BLL;
using DAL.Model;
using DAL.Repositories.Interfaces;
using DAL.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests;

public class ProjectBLTest
{
    private IQueryable<Project> repository;

    private Mock<IUnitOfWork> _unitOfWork;

    [SetUp]
    public void Setup()
    {
        InitDefaultRepository();

        Mock<IRepository<Project>> repositoryMock = new();

        repositoryMock.Setup(x => x.Read())
            .Returns(repository);

        repositoryMock.Setup(x => x.Filter(It.IsAny<Func<Project, bool>>()))
            .Returns((Func<Project, bool> predicate) => repository.Where(p => predicate(p)).AsQueryable());

        repositoryMock.Setup(x => x.Find(It.IsAny<Func<Project, bool>>()))
            .Returns((Func<Project, bool> predicate) => repository.FirstOrDefault(p => predicate(p)));

        repositoryMock.Setup(x => x.Create(It.IsAny<Project>()))
            .Callback((Project project) => repository = repository.Append(project));

        repositoryMock.Setup(x => x.Delete(It.IsAny<Project>()))
            .Callback((Project project) => repository = repository.Where(p => p.Id != project.Id).AsQueryable());


        _unitOfWork = new Mock<IUnitOfWork>();
        _unitOfWork.Setup(uow => uow.ProjectRepository).Returns(repositoryMock.Object);
    }

    [Test]
    public void GetAllProjects_RepositoryWith3Items_Success()
    {
        var projectBL = new ProjectBL(_unitOfWork.Object);

        var data = projectBL.GetAllProjects();

        Assert.IsTrue(data.Count == repository.Count());
    }

    [Test]
    public void FindProject_ByTitle_Success()
    {
        var projectBL = new ProjectBL(_unitOfWork.Object);
        repository = repository.Append(new Project()
        {
            Id = 15,
            Title = "Custom title",
            Description = "Custom description"
        });

        var data = projectBL.FindProject("Custom title");

        Assert.IsTrue(
            data.Count == 1 &&
            data[0].Title == "Custom title"
        );
    }

    [Test]
    public void FindProject_ByDescription_Success()
    {
        var projectBL = new ProjectBL(_unitOfWork.Object);
        repository = repository.Append(new Project()
        {
            Id = 15,
            Title = "Custom title",
            Description = "Custom description"
        });

        var data = projectBL.FindProject("Custom description");

        Assert.IsTrue(
            data.Count == 1 &&
            data[0].Description == "Custom description"
        );
    }

    [Test]
    public void CreateProject_NewProject_Success()
    {
        var projectBL = new ProjectBL(_unitOfWork.Object);

        var data = projectBL.CreateProject("New project", "New description");

        Assert.IsTrue(
            data.Title == "New project" &&
            data.Description == "New description" &&
            repository.Last().Title == data.Title &&
            repository.Last().Description == data.Description
        );
    }

    [Test]
    public void UpdateProject_NewProject_Success()
    {
        var projectBL = new ProjectBL(_unitOfWork.Object);

        var data = projectBL.UpdateProject(2, "Updated project", "Updated description");

        Assert.IsTrue(
            data.Title == "Updated project" &&
            data.Description == "Updated description" &&
            repository.First(p => p.Id == 2).Title == data.Title &&
            repository.First(p => p.Id == 2).Description == data.Description
        );
    }

    [Test]
    public void UpdateProject_Eror_Exception()
    {
        var projectBL = new ProjectBL(_unitOfWork.Object);


        Assert.Throws<ArgumentException>(projectBL.UpdateProject(2, "Updated project", "Updated description");)
        var data = 

        Assert.IsTrue(
            data.Title == "Updated project" &&
            data.Description == "Updated description" &&
            repository.First(p => p.Id == 2).Title == data.Title &&
            repository.First(p => p.Id == 2).Description == data.Description
        );
    }


    [Test]
    public void DeleteProject_Project1_Success()
    {
        var projectBL = new ProjectBL(_unitOfWork.Object);

        var data = projectBL.DeleteProject(1);

        Assert.IsTrue(data && !repository.Where(p => p.Id == 1).Any());
    }

    [Test]
    public void DeleteProject_DoesnotExist_Success()
    {
        var projectBL = new ProjectBL(_unitOfWork.Object);

        var data = projectBL.DeleteProject(15);

        Assert.IsTrue(data && !repository.Where(p => p.Id == 15).Any());
    }

    private void InitDefaultRepository()
    {
        repository = new List<Project>()
        {
            new Project() { Id = 1, Title = "Project1", Description = "Description 1" },
            new Project() { Id = 2, Title = "Project2", Description = "Description 2" },
            new Project() { Id = 3, Title = "Project3", Description = "Description 3" }
        }.AsQueryable();
    }
}
