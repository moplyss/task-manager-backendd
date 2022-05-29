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

public class AssignmentBlTest
{
    private IQueryable<Assignment> repository;

    private Mock<IUnitOfWork> _unitOfWork;

    [SetUp]
    public void Setup()
    {
        InitDefaultRepository();

        Mock<IRepository<Assignment>> repositoryMock = new();

        repositoryMock.Setup(x => x.Read())
            .Returns(repository);

        repositoryMock.Setup(x => x.Filter(It.IsAny<Func<Assignment, bool>>()))
            .Returns((Func<Assignment, bool> predicate) => repository.Where(p => predicate(p)).AsQueryable());

        repositoryMock.Setup(x => x.Find(It.IsAny<Func<Assignment, bool>>()))
            .Returns((Func<Assignment, bool> predicate) => repository.FirstOrDefault(p => predicate(p)));

        repositoryMock.Setup(x => x.Create(It.IsAny<Assignment>()))
            .Callback((Assignment project) => repository = repository.Append(project));

        repositoryMock.Setup(x => x.Delete(It.IsAny<Assignment>()))
            .Callback((Assignment project) => repository = repository.Where(p => p.Id != project.Id).AsQueryable());


        _unitOfWork = new Mock<IUnitOfWork>();
        _unitOfWork.Setup(uow => uow.AssignmentRepository).Returns(repositoryMock.Object);
    }

    [Test]
    public void GetAllAssignments_RepositoryWith3Items_Success()
    {
        var projectBL = new AssignmentBL(_unitOfWork.Object);

        var data = projectBL.GetAllAssignments();

        Assert.IsTrue(data.Count == repository.Count());
    }

    [Test]
    public void FindAssignment_ByTitle_Success()
    {
        var projectBL = new AssignmentBL(_unitOfWork.Object);
        repository = repository.Append(new Assignment()
        {
            Id = 15,
            Title = "Custom title",
            Description = "Custom description"
        });

        var data = projectBL.FindAssignment("Custom title");

        Assert.IsTrue(
            data.Count == 1 &&
            data[0].Title == "Custom title"
        );
    }

    [Test]
    public void FindAssignment_ByDescription_Success()
    {
        var projectBL = new AssignmentBL(_unitOfWork.Object);
        repository = repository.Append(new Assignment()
        {
            Id = 15,
            Title = "Custom title",
            Description = "Custom description"
        });

        var data = projectBL.FindAssignment("Custom description");

        Assert.IsTrue(
            data.Count == 1 &&
            data[0].Description == "Custom description"
        );
    }

    [Test]
    public void CreateAssignment_NewAssignment_Success()
    {
        var projectBL = new AssignmentBL(_unitOfWork.Object);

        var data = projectBL.CreateAssignment("New project", "New description", 15);

        Assert.IsTrue(
            data.ProjectId == 15 &&
            data.Title == "New project" &&
            data.Description == "New description" &&
            repository.Last().ProjectId == data.ProjectId &&
            repository.Last().Title == data.Title &&
            repository.Last().Description == data.Description
        );
    }

    [Test]
    public void UpdateAssignment_NewAssignment_Success()
    {
        var projectBL = new AssignmentBL(_unitOfWork.Object);

        var data = projectBL.UpdateAssignment(2, "Updated project", "Updated description", Status.Doing.ToString());

        Assert.IsTrue(
            data.Title == "Updated project" &&
            data.Description == "Updated description" &&
            data.Status == Status.Doing.ToString() &&
            repository.First(p => p.Id == 2).Title == data.Title &&
            repository.First(p => p.Id == 2).Description == data.Description &&
            repository.First(p => p.Id == 2).Status == Status.Doing
        );
    }

    [Test]
    public void UpdateAssignment_NotExistAssignment_Success()
    {
        var projectBL = new AssignmentBL(_unitOfWork.Object);

        Assert.Throws<ArgumentException>(() => projectBL.UpdateAssignment(15, "Updated project", "Updated description", Status.Doing.ToString()));
    }

    [Test]
    public void DeleteAssignment_Assignment1_Success()
    {
        var projectBL = new AssignmentBL(_unitOfWork.Object);

        var data = projectBL.DeleteAssignment(1);

        Assert.IsTrue(data && !repository.Where(p => p.Id == 1).Any());
    }

    [Test]
    public void DeleteAssignment_DoesNotExist_Failure()
    {
        var projectBL = new AssignmentBL(_unitOfWork.Object);

        var data = projectBL.DeleteAssignment(15);

        Assert.IsTrue(data == false);
    }

    private void InitDefaultRepository()
    {
        repository = new List<Assignment>()
        {
            new Assignment() { Id = 1, Title = "Assignment1", Description = "Description 1", ProjectId = 1, Status = Status.Finished},
            new Assignment() { Id = 2, Title = "Assignment2", Description = "Description 2", ProjectId = 1, Status = Status.Doing },
            new Assignment() { Id = 3, Title = "Assignment3", Description = "Description 3", ProjectId = 1, Status = Status.Created}
        }.AsQueryable();
    }
}
