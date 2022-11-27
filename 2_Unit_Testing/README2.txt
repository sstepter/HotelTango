//This is unit tests for all applicable classes in the Hotel Tango App, a unit test project must be created within the Hotel Tango Project using MS Visual Studio.


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;
using System.Net;
using HotelTango.Models;
using HotelTango.Controllers;

namespace HotelTango.Tests
{
    [TestClass]
    public class TestCustomerController
    { 
        [TestMethod]
        public void CreateCustomer_ShouldCreateCustomer()
        {
            var context = new TestCustomerAppContext();
            context.Customer.Add(new Customer {Id = 1, FirstName = "Jane", LastName = "Doe", Address = "123 Fake St", City = "Fake", State = "IL", PostalCode = "60606", EmailAddress = "jdoe@fake.com", PhoneNumber = "999-999-9999" });
            context.Customer.Add(new Customer {Id = 2, FirstName = "Jon", LastName = "Doe", Address = "456 Fake St", City = "Fake", State = "AL", PostalCode = "60656", EmailAddress = "jdoe1@fake.com", PhoneNumber = "999-999-9999" });
            context.Customer.Add(new Customer {Id = 3, FirstName = "Jim", LastName = "Doe", Address = "79 Fake St", City = "Fake", State = "TX", PostalCode = "60605", EmailAddress = "jdoe2@fake.com", PhoneNumber = "999-999-9999" });
            
            var controller = new CustomerController(context);
            var result = controller.CreateCustomer() as TestProductDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void DeleteCustomer_ShouldDeleteCustomer()
        {
            var context = new TestCustomerAppContext();
            context.Customer.Delete(Customer {Id = 1});
            context.Customer.Delete(Customer {Id = 2});
            context.Customer.Delete(Customer {Id = 3});
            
            var controller = new CustomerController(context);
            var result = controller.DeleteCustomer() as TestProductDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void UpdateCustomer_ShouldUpdateCustomer()
        {
            var context = new TestCustomerAppContext();
            context.Customer.Update(Customer {Id = 1});
            context.Customer.Update(Customer {Id = 2});
            context.Customer.Update(Customer {Id = 3});
            
            var controller = new CustomerController(context);
            var result = controller.UpdateCustomer() as TestProductDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void GetCustomer_ShouldGetCustomer()
        {
            var context = new TestCustomerAppContext();
            context.Customer.Get(Customer {Id = 1});
            context.Customer.Get(Customer {Id = 2});
            context.Customer.Get(Customer {Id = 3});
            
            var controller = new CustomerController(context);
            var result = controller.GetCustomer() as TestProductDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }    
    }


    [TestClass]
    public class TestRoomTypeController
    { 
        [TestMethod]
        public void CreateRoomType_ShouldCreateRoomType()
        {
            var context = new TestRoomTypeAppContext();
            context.RoomType.Add(new RoomType {Id = 1, RoomTypeName = "Double Suite", BedType = "Double", NumberOfBeds = 2, RoomRate = 79});
            context.RoomType.Add(new RoomType {Id = 2, RoomTypeName = "Queen Suite", BedType = "Queen", NumberOfBeds = 2, RoomRate = 109});
            context.RoomType.Add(new RoomType {Id = 3, RoomTypeName = "King Suite", BedType = "King", NumberOfBeds = 2, RoomRate = 129});
            
            var controller = new CustomerController(context);
            var result = controller.CreateCustomer() as TestProductDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void DeleteRoomType_ShouldDeleteRoomType()
        {
            var context = new TestRoomTypeAppContext();
            context.RoomType.Delete(RoomType {Id = 1});
            context.RoomType.Delete(RoomType {Id = 2});
            context.RoomType.Delete(RoomType {Id = 3});
            
            var controller = new RoomTypeController(context);
            var result = controller.RoomTypeCustomer() as TestProductDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void UpdateRoomType_ShouldUpdateRoomType()
        {
            var context = new TestUpdateRoomTypeAppContext();
            context.RoomType.Update(RoomType {Id = 1});
            context.RoomType.Update(RoomType {Id = 2});
            context.RoomType.Update(RoomType {Id = 3});
            
            var controller = new RoomTypeController(context);
            var result = controller.UpdateRoomType() as TestProductDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void GetRoomType_ShouldGetRoomType()
        {
            var context = new TestUpdateRoomTypeAppContext();
            context.RoomType.Get(RoomType {Id = 1});
            context.RoomType.Get(RoomType {Id = 2});
            context.RoomType.Get(RoomType {Id = 3});
            
            var controller = new RoomTypeController(context);
            var result = controller.GetRoomType() as TestProductDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }    
    }


    [TestClass]
    public class TestRoom
    { 
        [TestMethod]
        public void CreateRoom_ShouldCreateRoom()
        {
            var context = new TestRoomAppContext();
            context.Room.Add(new Room {Id = 1, RoomNumber = "101", RoomTypeID = 1});
            context.Room.Add(new Room {Id = 2, RoomNumber = "102", RoomTypeID = 2});
            context.Room.Add(new Room {Id = 3, RoomNumber = "103", RoomTypeID = 3});
            
            var controller = new RoomController(context);
            var result = controller.CreateRoom() as TestProductDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void DeleteRoom_ShouldDeleteRoom()
        {
            var context = new TestRoomAppContext();
            context.Room.Delete(Room {Id = 1});
            context.Room.Delete(Room {Id = 2});
            context.Room.Delete(Room {Id = 3});
            
            var controller = new RoomController(context);
            var result = controller.Room() as TestProductDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void UpdateRoom_ShouldUpdateRoom()
        {
            var context = new TestRoomAppContext();
            context.Room.Update(Room {Id = 1});
            context.Room.Update(Room {Id = 2});
            context.Room.Update(Room {Id = 3});
            
            var controller = new RoomController(context);
            var result = controller.UpdateRoom() as TestProductDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void GetRoom_ShouldUpdateRoom()
        {
            var context = new TestRoomAppContext();
            context.Room.Get(Room {Id = 1});
            context.Room.Get(Room {Id = 2});
            context.Room.Get(Room {Id = 3});
            
            var controller = new GetController(context);
            var result = controller.GetRoom() as TestProductDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }    
    }    


    //Reservation Class
    [TestClass]
    public class TestReservation
    { 
        [TestMethod]
        public void CreateReservation_ShouldCreateReservation()
        {
            var context = new TestRoomAppContext();
            context.Reservation.Add(new Reservation {Id = 1, CustomerID = 1, RoomTypeID = 1, WIFI_Passcode = "yala", StartDate = "2022/10/22", EndDate = "2022/10/27"});
            context.Reservation.Add(new Reservation {Id = 2, CustomerID = 2, RoomTypeID = 2, WIFI_Passcode = "habibi", StartDate = "2022/10/23", EndDate = "2022/10/28"});
            context.Reservation.Add(new Reservation {Id = 3, CustomerID = 3, RoomTypeID = 3, WIFI_Passcode = "test", StartDate = "2022/10/24", EndDate = "2022/10/29"});
            
            var controller = new ReservationController(context);
            var result = controller.CreateReservation() as TestProductDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void DeleteReservation_ShouldDeleteReservation()
        {
            var context = new TestRoomAppContext();
            context.Reservation.Delete(Reservation {Id = 1});
            context.Reservation.Delete(Reservation {Id = 2});
            context.Reservation.Delete(Reservation {Id = 3});
            
            var controller = new ReservationController(context);
            var result = controller.Reservation() as TestProductDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void UpdateReservation_ShouldUpdateReservation()
        {
            var context = new TestReservationAppContext();
            context.Reservation.Update(Reservation {Id = 1});
            context.Reservation.Update(Reservation {Id = 2});
            context.Reservation.Update(Reservation {Id = 3});
            
            var controller = new ReservationController(context);
            var result = controller.UpdateReservation() as TestProductDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void GetReservation_ShouldGetReservation()
        {
            var context = new TestReservationAppContext();
            context.Reservation.Get(Reservation {Id = 1});
            context.Reservation.Get(Reservation {Id = 2});
            context.Reservation.Get(Reservation {Id = 3});
            
            var controller = new ReservationController(context);
            var result = controller.GetReservation() as TestProductDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }
    }            

