using System;
using DataAccessLayer.Repository;
using DataAccessLayer;
using ModelLayer;
using System.Data;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Controllers;
using System.Data.Common;
using System.Collections.Generic;
using WebApplication.Models;
using System.Linq;
using RoomModel = ModelLayer.RoomModel;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestClass]
    public class RoomTest
    {
        private RoomRepository roomDb;
        private readonly string connString = "Data Source = hildur.ucn.dk; Initial Catalog = dmaj0919_1081489; User ID = dmaj0919_1081489; Password=Password1!;Connect Timeout = 60; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public RoomTest()
        {
            roomDb = new RoomRepository();
        }

        public void RoomControllerTest()
        {
            Assert.AreEqual("RoomsController", "RoomsController");
        }
       

        //Test for creating a room
        [TestMethod]
        public void CreateRoomTest()
        {
            //Arrange
            RoomModel room = new RoomModel();
            room.RoomNumber = 1;
            room.Price = 1200;
            room.IsAvailable = true;
            room.Floor = 3;
            room.Capacity = 1;
            room.Area = 20;
            room.Description = "furnished";
            //Act
            roomDb.Add(room);
            //Assert
            Assert.IsNotNull(room);
        }

        

        //Test method for getting all rooms in the database
        [TestMethod]
        public void GetRoomsTest()
        {
            //Arrange
            List<ModelLayer.RoomModel> rooms = (List<ModelLayer.RoomModel>)roomDb.GetAll();
            //Act
            int roomsTotal = 0;
            if (rooms is List<ModelLayer.RoomModel>)
            {
                roomsTotal = rooms.Count;
            }
            //Assert
            Assert.IsTrue(roomsTotal > 0);
        }

        //Test method for getting all available rooms in the database
        [TestMethod]
        public void GetAllAvailableRoomsTest()
        {
            //Arrange
            List<ModelLayer.RoomModel> rooms = (List<ModelLayer.RoomModel>)roomDb.GetAllAvailable();
            //Act
            int roomsTotal = 0;
            if (rooms is List<ModelLayer.RoomModel>)
            {
                roomsTotal = rooms.Count;
            }
            //Assert
            Assert.IsTrue(roomsTotal > 0);
        }

        //Test method for geting a room with its id
        [TestMethod]
        public void GetRoomByIdTest()
        {
            //Arrange
            List<ModelLayer.RoomModel> rooms = (List<ModelLayer.RoomModel>)roomDb.GetAll();
            //Act
            int total = rooms.Count;
            int id = rooms[total - 1].Id;
            ModelLayer.RoomModel room = roomDb.Find(id);
            //Assert
            Assert.AreEqual(room.Id, id);
        }

        //Test method for updating a room
        [TestMethod]
        public void UpdateRoomTest()
        {
            //Arrange
            List<ModelLayer.RoomModel> rooms = (List<ModelLayer.RoomModel>)roomDb.GetAll();
            //Act
            int total = rooms.Count;
            int id = rooms[total - 1].Id;
            ModelLayer.RoomModel room = roomDb.Find(id);
            room.Price = 1800;
            roomDb.Update(room);
            //Assert
            Assert.IsNotNull(room);
        }

        //Test method for removing a room from the database
        [TestMethod]
        public void RemoveRoomTest()
        {
            //Arrange
            List<ModelLayer.RoomModel> rooms = (List<ModelLayer.RoomModel>)roomDb.GetAll();
            //Act
            int total = rooms.Count;
            int id = rooms[total - 1].Id;
            var room = new RoomModel();
            roomDb.Remove(room.Id);
            //Assert
            Assert.AreNotEqual(room.Id, id);
        }

        //Test method for testing the view returned by the controller
        [TestMethod]
        public void RoomsViewTest()
        {
            Task.Run(async () =>
            {
                RoomsController roomscontroller = new RoomsController();
                var rooms = await roomscontroller.Rooms("", "");
                //assert
            }).GetAwaiter().GetResult();
            
        }
    }
}
