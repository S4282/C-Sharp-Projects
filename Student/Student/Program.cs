using System;
using System.IO;
using System.Linq;

namespace Student
{

    public class StudentRecord 
    {
        // Protecting Data //
        private string firstname;
        private string lastname;
        private double final;
        private double[] grade = new double[8];

        public StudentRecord() // Default Constructor //
        {
            firstname = "";
            lastname = "";
            grade = new double[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }

        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }

        }
        public double[] Grade
        {
            get { return grade; }
            set { grade = value; }
        }
        public double Final
        {
            get { return final; }
            set { final = value; }
        }
        public StudentRecord(string studentlname, string studentfname, double[] grade)
        {
            FirstName = studentfname;
            LastName = studentlname;  // Instance of copy constructor //
            Grade = grade;
        } 

        public static void DisplayAllGrades(int numofstudents, StudentRecord[] record)
        {
            int i;
            for (i = 0; i < record.Length; ++i)
            {

                if (record[i].LastName.ToString().Length <= 6) // Add a tab if last name is more than or equal to 6 characters //
                {
                        // Final is calculated by taking weighted average of grades and coming to the final //
                        record[i].Final = (((15 * record[i].Grade[0]) + (15 * record[i].Grade[1]) + (20 * record[i].Grade[2]) + (10 * record[i].Grade[3]) + (10 * record[i].Grade[4]) + (10 * record[i].Grade[5]) + (10 * record[i].Grade[6]) + (10 * record[i].Grade[7])) / (100));
                        Console.WriteLine("LastName,\tFirstName:\tExam1\tExam2\tExam3\tLab1\tLab2\tLab\tLab4\tLab5\tFinal");
                        Console.WriteLine("{0},\t\t{1}\t\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}", record[i].LastName, record[i].FirstName, record[i].Grade[0], record[i].Grade[1], record[i].Grade[2], record[i].Grade[3], record[i].Grade[4], record[i].Grade[5], record[i].Grade[6], record[i].Grade[7], record[i].Final);
                }
               else
                {
                    record[i].Final = (((15 * record[i].Grade[0]) + (15 * record[i].Grade[1]) + (20 * record[i].Grade[2]) + (10 * record[i].Grade[3]) + (10 * record[i].Grade[4]) + (10 * record[i].Grade[5]) + (10 * record[i].Grade[6]) + (10 * record[i].Grade[7])) / (100));
                    Console.WriteLine("LastName,\tFirstName:\tExam1\tExam2\tExam3\tLab1\tLab2\tLab\tLab4\tLab5\tFinal");
                    Console.WriteLine("{0},\t{1}\t\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}", record[i].LastName, record[i].FirstName, record[i].Grade[0], record[i].Grade[1], record[i].Grade[2], record[i].Grade[3], record[i].Grade[4], record[i].Grade[5], record[i].Grade[6], record[i].Grade[7], record[i].Final);
                }
                Console.WriteLine(" "); 
                }

        }

        
        
    }

    class Program
    {
        public static void Main() // Prompts for number of users in the array and sets the value //
        {
            int numofstudents;
            int x;
            Console.WriteLine("Input from keyboard (Enter 1) or file (Enter 2)? >> ");
            int.TryParse(Console.ReadLine(), out x);
            if (x == 1)
            {
                Console.WriteLine("How many students? >> ");
                while (!int.TryParse(Console.ReadLine(), out numofstudents))
                {
                    Console.WriteLine("Enter valid value.");
                    Console.WriteLine("How many students? >> ");
                    
                }
                
                StudentRecord[] record = new StudentRecord[numofstudents];
                Readdata(numofstudents, record);
                StudentRecord.DisplayAllGrades(numofstudents, record);
                sortStudents(record);
                searchHighestGrade(record);
            }
            else if (x == 2)
            {
                readFileData();
            }
            
            else
            {
                Console.WriteLine("Please enter a valid input.");
                Main();
            }
        }

        private static void readFileData()
        {
            //const char DELIM = ' '; null is better //

            string FILENAME;
            string recordIn;
            string[] fields;
            int numofstudents;
            Console.Write("Enter file name >> ");
            FILENAME = Console.ReadLine();
            if (File.Exists(FILENAME))
            {
                Console.WriteLine(" ");
                FileStream inFile = new FileStream(FILENAME, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(inFile);
                numofstudents = Convert.ToInt32(reader.ReadLine()); // Grab first number of file and convert it to integer for array size //

                if (numofstudents + 1 < File.ReadAllLines(FILENAME).Count() || numofstudents + 1 > File.ReadAllLines(FILENAME).Count()) // Checks that the first line of txt file is not less or more to the number of studenst //
                {
                    Console.WriteLine("Invalid file format.");
                    readFileData();
                }
                
                recordIn = reader.ReadLine();
                StudentRecord[] record = new StudentRecord[numofstudents]; // Set array amount to number of students from first line //
                while (recordIn != null)
                {
                   
                    for (int x = 0; x < record.Length; x++) // Increment the number of students to set each value for array //
                    {
                        fields = recordIn.Split(null); // Split data by null values or spaces //
                        double[] grades = { Convert.ToDouble(fields[2]), Convert.ToDouble(fields[3]), Convert.ToDouble(fields[4]), Convert.ToDouble(fields[5]), Convert.ToDouble(fields[6]), Convert.ToDouble(fields[7]), Convert.ToDouble(fields[8]), Convert.ToDouble(fields[9]) }; // Set grade variables from txt file //
                        record[x] = new StudentRecord(fields[1], fields[0], grades); // Increment value in array and set values //
                        recordIn = reader.ReadLine();

                    }
                    rankingSort(record);
                }
                
                reader.Close();
                inFile.Close();

            }
            else
            {
                Console.WriteLine("File {0} does not exist.", FILENAME);
                readFileData();
            }
        }

        private static void rankingSort(StudentRecord[] record) // Rank data by highest final grade //
        {
            int x, j;
            double tmp;
            // Calculate all finals grades for comparison //
            for (int i = 0; i < record.Length; i++)
            {
                record[i].Final = (((15 * record[i].Grade[0]) + (15 * record[i].Grade[1]) + (20 * record[i].Grade[2]) + (10 * record[i].Grade[3]) + (10 * record[i].Grade[4]) + (10 * record[i].Grade[5]) + (10 * record[i].Grade[6]) + (10 * record[i].Grade[7])) / (100));
            }
            
            // Sorting algorithm for Ascending Order //
            for (x = 0; x < record.Length; x++)
            {
                for (j = x + 1; j < record.Length; j++)
                {
                    if (record[j].Final > record[x].Final)
                    {
                        tmp = record[x].Final;
                        record[x].Final = record[j].Final;
                        record[j].Final = tmp;
                    }
                }
            }
            displayRank(record);
        }

        private static void displayRank(StudentRecord[] record) // Writes rank to txt file and to screen //
        {
            const string FILENAME = "gradeRanking.txt"; // Setting filename as constant //
            for (int x = 0; x < record.Length; x++)
            {
                using (StreamWriter writer = new StreamWriter(FILENAME)) // Establishing streamwriter to write to file //
                {
                    writer.WriteLine("# of ranks = {0}", record.Length);
                    writer.WriteLine("------------------------");
                    for (x = 0; x < record.Length; x++)
                    {
                        writer.WriteLine("#" + (x + 1) + " {0} {1} {2}", record[x].LastName, record[x].FirstName, record[x].Final); // Write rank to txt file along with number. (x + 1) is due to array starting at zero we need to start at 1 //
                    }
                    Console.WriteLine("# of ranks = {0}", record.Length);
                    Console.WriteLine("------------------------");
                    for (x = 0; x < record.Length; x++) 
                    {
                        Console.WriteLine("#" + (x + 1) + " {0} {1} {2}", record[x].LastName, record[x].FirstName, record[x].Final); // Writing same format to screen as file //
                    }
                }
                display(record);
            }
        }

        private static void display(StudentRecord[] record) // Use DisplayAllGrades function to display to screen and write to studentRecord.txt //
        {
            const string FILENAME = "studentRecord.txt"; // Setting filename as constant //
            Console.WriteLine(" ");
            StudentRecord.DisplayAllGrades(record.Length, record); // Use DisplayAllGrades() function to print to console //
            using (var writer = new StreamWriter(FILENAME))
            {
                
                Console.SetOut(writer); // Sets output to writer //
                StudentRecord.DisplayAllGrades(record.Length, record); // Use DisplayAllGrades() function to be copied to new file  //
                Console.SetOut(Console.Out); // Wrote last Console function's text to txt file //
            }
            
        }

        private static void Readdata(int numofstudents, StudentRecord[] record) // Reading data if user choses keyboard input //
        {
            
            string lastname;
            string firstname;
            int x;
            for (x = 0; x < record.Length; ++x) // Increment the array value to determine values for each student //
            {
                double[] grades = new double[8];
                Console.WriteLine("Enter student's last name >> ");
                lastname = Console.ReadLine();
                Console.WriteLine("Enter student's first name >> ");
                firstname = Console.ReadLine();
                Console.WriteLine("Enter student's exam 1 grade >> ");
                grades[0] = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter student's exam 2 grade >> ");
                grades[1] = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter student's exam 3 grade >> ");
                grades[2] = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter student's lab 1 grade >> ");
                grades[3] = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter student's lab 2 grade >> ");
                grades[4] = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter student's lab 3 grade >> ");
                grades[5] = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter student's lab 4 grade >> ");
                grades[6] = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter student's lab 5 grade >> ");
                grades[7] = Convert.ToDouble(Console.ReadLine());
                record[x] = new StudentRecord(lastname, firstname, grades);

            }


        }
        private static void sortStudents(StudentRecord[] record) // Sorting algorithm that checks for alphabetical order and presents in LastName then FirstName //
        {
            for (int i = 1; i < record.Length; i++)
            {
                for (int j = 0; j < record.Length - 1; j++)
                {
                    string lastName1 = record[j].LastName.ToLower() + record[j].FirstName.ToLower();
                    string lastName2 = record[j + 1].LastName.ToLower() + record[j + 1].FirstName.ToLower();
                    int shortestNameLength = Math.Min(lastName1.Length, lastName2.Length);
                    for (int k = 0; k < shortestNameLength; k++)
                    {
                        int c1 = (lastName1[k]);
                        int c2 = (lastName2[k]);
                        if (c1 == c2)
                        {
                            continue;
                        }
                        if (c1 > c2)
                        {
                            StudentRecord currentStudent = record[j];
                            record[j] = record[j + 1];
                            record[j + 1] = currentStudent;
                        }
                        break;
                    }
                }
            }
            Console.WriteLine("List of Students by Last then First & in Alphabetical Order:");
            for (int i = 0; i < record.Length; i++)
            {
                Console.WriteLine(string.Format("{0} {1}", record[i].LastName, record[i].FirstName));
            }
        }
        private static void searchHighestGrade(StudentRecord[] record) // Sorting algorithm to find the highest grade of the array //
        {
            int maxIndex = -1;
            double maxInt = Double.MinValue;

            for (int i = 0; i < record.Length; i++)
            {
                double value = record[i].Final;
                if (value > maxInt)
                {
                    maxInt = value;
                    maxIndex = i;
                }
            }
            Console.WriteLine(" ");
            Console.WriteLine("Highest grade in IST311.Students is " + record[maxIndex].LastName + ", " + record[maxIndex].FirstName + "'s " + "final grade " + maxInt + ".");


        }
        
    }
}
