using DataAccessLayer;
using DataAccessLayer.Repository;
using ModelLayer;
using System;
using System.Diagnostics;
using System.Linq;

namespace Runner
{
    class Program
    {

        static void Main(string[] args)
        {
            //Initialize();

            //Get_all_should_return_all_entities();

            //Insert_should_assign_identity_to_new_entity();

            //Find_should_retrieve_existing_entity(2);

            //Modify_should_update_existing_entity(4);

            //Delete_should_remove_entitiy(4);

            //Insert_full_booking_should_update_all_related_tables();

            //Insert_should_assign_identity_to_new_entity();

            //Insert_admin_should_add_new_entity();

            //Insert_room_should_add_new_entity();

            //Get_all_available_rooms_should_return_all();

            //Get_specific_room_should_return_1_result(1);

            Get_All_Student_Bookings(3);

            Console.ReadLine();
        }

        static void Get_All_Student_Bookings(int studentId)
        {
            //Arrange 
            var bookingRepository = CreateBookingRepository();

            //Act
            var bookings = bookingRepository.GetAll(studentId);

            //assert
            Console.WriteLine($"Count: {bookings.ToList().Count}");
            Debug.Assert(bookings.ToList().Count == 2);
            bookings.Output();
        }

        static void Insert_room_should_add_new_entity()
        {
            //Arrange
            var roomRepository = CreateRoomRepository();

            //Act
            var room = new RoomModel()
            {
                RoomNumber = 202,
                Floor = 2,
                Capacity = 1,
                Area = 38,
                Price = 2700,
            };
            roomRepository.Add(room);
            //Assert
            Debug.Assert(room.Id != 0);
            if(room.Id != 0){
                Console.WriteLine("***Room inserted***");
            }
            Console.WriteLine($"ID: {room.Id}");

        }

        private static void Get_all_available_rooms_should_return_all()
        {
            //arrange
            var roomRepository = CreateRoomRepository();

            //act
            var availableRooms = roomRepository.GetAllAvailable();

            var rooms = roomRepository.GetAll();

            //assert
            Console.WriteLine($"Count: {rooms.ToList().Count}");
            //Debug.Assert(bookings.Count == 1);
            rooms.Output();

        }

        private static void Get_specific_room_should_return_1_result(int id)
        {
            //arrange
            var roomRepository = CreateRoomRepository();

            //act
            var room = roomRepository.Find(id);

            //assert
            Debug.Assert(room != null);
            room.Output();

        }

        private static IRoomRepository CreateRoomRepository()
        {
            return new RoomRepository();
        }

        private static IStudentRepository CreateStudentRepository()
        {
            return new StudentRepository();
        }

        //private static AdministratorRepository CreateAdministratorRepository()
        //{
        //    return new AdministratorRepository();
        //}


        private static IBookingRepository CreateBookingRepository()
        {
            return new BookingRepository();
        }

 

        private static void Get_all_should_return_all_entities()
        {
            //arrange
            //var studentRepository = CreateStudentRepository();
            var bookingRepository = CreateBookingRepository();

            //act
            var bookings = bookingRepository.GetAll();
            //var students = studentRepository.GetAll();

            //assert
            Console.WriteLine($"Count: {bookings.ToList().Count}");
            Debug.Assert(bookings.ToList().Count == 0);
            bookings.Output();

            //Console.WriteLine($"Count: {students.Count}");
            //Debug.Assert(students.Count == 1);
            //students.Output();
        }

        private static void Find_should_retrieve_existing_entity(int id)
        {
            //arrange 
            var bookingRepository = CreateBookingRepository();

            //act
            var booking = bookingRepository.Find(id);

            Console.WriteLine("***Get Booking***");
            booking.Output();
            //Debug.Assert(booking.MoveInDate == DateTime.Parse("2021-01-01"));
            //Debug.Assert(booking.MoveOutDate == DateTime.Parse("2021-03-15"));
        }

        private static int Insert_should_add_new_entity()
        {
            //arrange
            var bookingRepository = CreateBookingRepository();
            var booking = new BookingModel
            {
                MoveInDate = DateTime.Parse("2021-12-21"),
                MoveOutDate = DateTime.Parse("2022-11-11"),
                Status = BookingStatus.Pending
            };

            //act
            bookingRepository.AddAnonymous(booking);

            //assert 
            Debug.Assert(booking.Id != 0);
            Console.WriteLine("***Booking inserted***");
            Console.WriteLine($"New ID: {booking.Id}");
            return booking.Id;
        }

        
    }
}
