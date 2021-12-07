using System;
using System.Collections.Generic;
using Roommates.Repositories;
using Roommates.Models;
using Roomates.Repositories;
using System.Linq;


namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true;TrustServerCertificate=true";

        

        static void Main(string[] args)
        {

            Roommate roommate1 = new Roommate("Leith", "Abudiab", 80, new DateTime(2021, 5, 5), new Room() { });
            Roommate roommate2 = new Roommate()
            {
                FirstName = "Bob",
                LastName = "Joshson",
                RentPortion = 50,
                MovedInDate = new DateTime(2021, 12, 5),
                Room = new Room() { },
            };

            Console.WriteLine(roommate1.Details);
            Console.WriteLine(roommate2.Details);
            Console.ReadKey();

            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoomateRepository roomateRepo = new RoomateRepository(CONNECTION_STRING);

            bool runProgram = true;

            while (runProgram)
            {

                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for room"):
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);
                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Delete a room"):

                        List<Room> roomDelete = roomRepo.GetAll();
                        foreach (Room r in roomDelete)
                        {
                            Console.WriteLine($"{r.Id} = {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }

                        Console.Write("Which room would you like to delete? ");
                        int deleteRoomId = int.Parse(Console.ReadLine());

                        roomRepo.Delete(deleteRoomId);

                        Console.WriteLine("Room has been deleted");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Update a room"):
                        List<Room> roomOptions = roomRepo.GetAll();
                        foreach (Room r in roomOptions)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }

                        Console.Write("Which room would you like to update? ");
                        int selectedRoomId = int.Parse(Console.ReadLine());
                        Room selectedRoom = roomOptions.FirstOrDefault(r => r.Id == selectedRoomId);

                        Console.Write("New Name: ");
                        selectedRoom.Name = Console.ReadLine();

                        Console.Write("New Max Occupancy: ");
                        selectedRoom.MaxOccupancy = int.Parse(Console.ReadLine());

                        roomRepo.Update(selectedRoom);

                        Console.WriteLine("Room has been successfully updated");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case "Show all chores":
                        List<Chore> chores = choreRepo.GetAll();

                        foreach (Chore c in chores)
                        {
                            Console.WriteLine($"{c.Name} has an Id of {c.Id}");
                        }
                        Console.WriteLine("Press any key to continue");
                        Console.ReadLine();
                        break;
                    case "Search for chore":
                        int choreId = int.Parse(Console.ReadLine());

                        Chore chore = choreRepo.GetById(choreId);

                        Console.WriteLine($"{chore.Id} - {chore.Name} ");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadLine();
                        break;
                    case "Add a chore":
                        Console.Write("Enter chore name: ");
                        string choreName = Console.ReadLine();

                        Chore newChore = new Chore
                        {
                            Name = choreName
                        };

                        choreRepo.Insert(newChore);

                        Console.WriteLine($"{newChore.Name} has been added and assigned an Id of {newChore.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case "Select a roommate":
                        int roommateId = int.Parse(Console.ReadLine());

                        Roommate newRoommate = roomateRepo.GetById(roommateId);

                        Console.WriteLine($"{newRoommate.Id} - {newRoommate.FirstName} is in the {newRoommate.Room.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadLine();
                        break;
                    case ("Exit"):
                        runProgram = false;
                        break;
                }
            }

        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Search for room",
                "Add a room",
                "Delete a room",
                "Update a room",
                "Show all chores",
                "Search for chore",
                "Add a chore",
                "Select a roommate",
                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }

    }
}

