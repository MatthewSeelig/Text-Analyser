using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Gives accesss to the classes needed to read text from file
using System.IO;
//Needed in order to use the Regex.Replace method when calculating number of sentances from ReadAllText(Filename)
using System.Text.RegularExpressions;

namespace Analysis_of_Text
{
    //This class contains the 'main' method, as well as global variables and various console prints 
    class Program
    {
        // Variables declared outside of the main method so that the text entry is easy to retrieve, use and analyse by the other class
        public static bool flag = true; /* variable used in the option selection */
        public static bool asterisk = false; /* variable used to signify the end of the entry */
        public static List<string> textEntryArray = new List<String>(); /* List<> data structure that hold all of the text entries and the text read from file */
        public static string textEntry; /* String that holds the text read from file but as one whole string, not an array */
        public static string manualTextEntry; /* variable for each string entered */
        public static int numOfSentances; /* variable counter used in the analysis of the number of sentances */


        //Main method
        static void Main(string[] args)
        {
            //while loop checking for selected menu option
            while (flag)
            {
                //Menu method is called - this enables the user to select an option
                UserInput.Menu();

                //Option 1 is selected
                if (UserInput.selectOption == 1)
                {
                    //These variables are set to there default value so that the program doesn't use past text entries or any other past text analysis
                    numOfSentances = 0;
                    textEntryArray.Clear();
                    //Asterisk is set to false so that while loop iterates
                    asterisk = false;

                    //Text entry via keyboard
                    Console.WriteLine("\nPlease enter one or more sentances of text, one sentance at a time.");
                    Console.WriteLine("Place an asterisk (*) to indicate the end of the entry.");

                    //while loop that allows for multiple sentances to be analysed until an '*' is observed
                    while (!asterisk)
                    {
                        Console.Write("\n\ttext: ");

                        //Storing the entered text as a string variable
                        manualTextEntry = Console.ReadLine();

                        //If the text entry contains only an asterisk, it is not counted as a sentance and the text entry ends
                        if (manualTextEntry == "*")
                        {
                            asterisk = true;
                        }
                        //If the text entry is empty, it is not counted as a sentance and the text entry continues
                        else if (manualTextEntry == "")
                        {
                            continue;
                        }
                        else
                        {
                            //Storing the entered text in the List<> data structure
                            textEntryArray.Add(manualTextEntry);
                            //Incrementing the number of sentances (User is told to enter one sentance at a time)
                            numOfSentances++;

                            //Changes the asterisk bool to true, signifying the end of the entry and breaking out of the loop
                            asterisk = manualTextEntry.EndsWith("*");
                        }
                    }

                    //Outputs of the text analysis that is carried out in the 'textAnalysis' class
                    //Methods are called within string, or on their own, depending on their data type
                    Console.WriteLine("\n\t\t\t*******************");
                    Console.WriteLine("\t\t\t***TEXT ANALYSIS***");
                    Console.WriteLine("\t\t\t*******************");
                    Console.WriteLine("\t\t\tNumber of sentances = {0}", numOfSentances);
                    Console.WriteLine("\t\t\tNumber of vowels = {0}", TextAnalysis.VowelCount());
                    Console.WriteLine("\t\t\tNumber of consonants = {0}", TextAnalysis.ConsonantCount());
                    Console.WriteLine("\t\t\tNumber of upper case letters = " + TextAnalysis.UpperCount());
                    Console.WriteLine("\t\t\tNumber of lower case letters = " + TextAnalysis.LowerCount());
                    Console.WriteLine("\n\t\t\tFrequency of Letters");
                    TextAnalysis.FreqOfLetters();
                    Console.WriteLine("\n");

                    //Flag is set to true (incase it was set to false) in order for the user to be given the two options again (first while loop)
                    flag = true;

                }


                //Option 2 is selected
                else if (UserInput.selectOption == 2)
                {
                    //These variables are set to there default value so that the program doesn't use past text entries or any other past text analysis
                    numOfSentances = 0;
                    textEntryArray.Clear();
                    textEntry = string.Empty;
                    //Asterisk is set to false so that while loop iterates
                    asterisk = false;

                    Console.WriteLine("\nYou have chosen to read in the text from a file.");

                    //Event Handling is used in case the file cannot be read
                    try
                    {
                        //File containing pre-written text is opened, read and stored in an array
                        textEntryArray = File.ReadAllLines(@"..\..\Text Files\CMP1127M Assignment 1 Text.txt").ToList(); /* Holds the text from file that is used to calculate number of vowels, consonants, Upper and lowercase letters, and the frequency of letters. This data structure is also used to create the Long Words file */
                        textEntry = File.ReadAllText(@"..\..\Text Files\CMP1127M Assignment 1 Text.txt").Trim(); /* Holds the text from file that is used to calculate number of sentances - The rest of the text analysis is done on File.ReadAllLines(Filename) - Trim is used to remove initial whitespace*/
                    }

                    //An error message is produced and the analysis does not occur (due to the break)
                    catch (Exception ex)
                    {
                        Console.WriteLine("\nSomething went wrong: " + ex.Message + "\n");
                        Console.WriteLine("The file name or path may be incorrect - both of these can be changed in the 'Program.cs' file");
                        break;
                    }


                    //Outputs of the text analysis that is carried out in the 'textAnalysis' class
                    //Methods are called within string, or on their own, depending on their data type
                    Console.WriteLine("\n\t\t\t*******************");
                    Console.WriteLine("\t\t\t***TEXT ANALYSIS***");
                    Console.WriteLine("\t\t\t*******************");
                    TextAnalysis.NumOfSentancesReadAllText();
                    Console.WriteLine("\t\t\tNumber of vowels = {0}", TextAnalysis.VowelCount());
                    Console.WriteLine("\t\t\tNumber of consonants = {0}", TextAnalysis.ConsonantCount());
                    Console.WriteLine("\t\t\tNumber of upper case letters = " + TextAnalysis.UpperCount());
                    Console.WriteLine("\t\t\tNumber of lower case letters = " + TextAnalysis.LowerCount());
                    Console.WriteLine("\n\t\t\t*Frequency of characters*");
                    TextAnalysis.FreqOfLetters();
                    Console.WriteLine("\n\t\t\tA \"long words\" text file is being created...");
                    TextAnalysis.WriteLongWords();
                    Console.WriteLine("\n\t\t\tA text file containing words longer than seven letters in length has been created");
                    Console.WriteLine("\n");

                    //Flag is set to true (incase it was set to false) in order for the user to be given the two options again (first while loop)
                    flag = true;

                }


                //Option 3 is selected
                else if(UserInput.selectOption == 3)
                {
                    //Flag is set to false to stop the while loop from reiterating
                    flag = false;

                    //Closing the program
                    Console.WriteLine("\n\t\t\t\t...");
                    Console.WriteLine("\nThe program will now close");
                    Console.WriteLine("\nPlease press any key...");
                    Console.ReadKey();
                }


                //Neither option 1 or 2 is selected - The exception handler will not produce an error if an integer is entered (as it is still the correct data type) so this is necessary
                else if ((UserInput.selectOption != 1 || UserInput.selectOption != 2 || UserInput.selectOption != 3) && !UserInput.error)
                {
                    Console.Write("\n\tINVALID OPTION! Please choose either option 1 or option 2\n\n");
                    //Keeping flag true so that the while loop reiterates
                    flag = true;
                }


            }
        }
    }



    public class UserInput
    {
        public static int selectOption; /* variable that holds the selected option from the menu */
        public static bool error = false; /* variable used in the menu options exception handling so that two errors aren't produced */

        public static void Menu()
        {
            //Exception handler implemented incase user enters incorrect format e.g. characters 
            try
            {
                //As exception handing and an if statement both produce errors - I have used a bool to make sure only one error is produced at a time
                error = false;
                Console.WriteLine("1. Do you want to enter the text via the keyboard?");
                Console.WriteLine("2. Do you want to read in the text from a file?");
                Console.WriteLine("3. Do you want to close the program?");
                Console.Write("Option: ");
                selectOption = Convert.ToInt32(Console.ReadLine());
            }

            //If an exception is caught an error message is produced 
            catch (Exception ex)
            {
                //Error becomes true and the second error message  (the else statement below) will not be produced
                error = true;
                Console.WriteLine("\n\tSomething went wrong: " + ex.Message + "\n");
            }
        }
    }



    //This class contains all of the methods and data structures used in the analysis of the text
    public class TextAnalysis
    {
        //Vowel and Consonant Arrays
        public static char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
        public static char[] consonants = { 'b','c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };


        //Vowel count method
        public static int VowelCount()
        {            
            int count = 0; /* count holds the amount of vowels */

            //this first for loop cycles through each string that is stored in the textEntryArray List<> 
            for(int i = 0; i < Program.textEntryArray.Count; i++)
            {
                //This if statement is necessary in order to prevent the compiler from failing if it reaches an element in the array that is a 'null' value
                if(Program.textEntryArray[i] == null)
                {
                    break;
                }
                
                //This variable is assigned the current string that the for loop has cycled onto
                string currentString = Program.textEntryArray[i];
                    
                //Count adds and is assigned the amount of vowels contained in the current string - the lammbda operator is used to make the code more efficent
                count += currentString.Count(y => TextAnalysis.vowels.Contains(Char.ToLower(y)));
            }

            //count is returned once an element in the array is 'null'
            return (count);
        }


        //Consonant count method
        public static int ConsonantCount()
        {
            int count = 0; /* count holds the amount of consonants */

            //this first for loop cycles through each string that is stored in the textEntryArray List<> 
            for (int i = 0; i < Program.textEntryArray.Count; i++)
            {
                //This if statement is necessary in order to prevent the compiler from failing if it reaches an element in the array that is a 'null' value
                if (Program.textEntryArray[i] == null)
                {
                    break;
                }

                //This variable is assigned the current string that the for loop has cycled onto
                string currentString = Program.textEntryArray[i];

                //Count adds and is assigned the amount of consonants contained in the current string - the lammbda operator is used to make the code more efficent
                count += currentString.Count(y => TextAnalysis.consonants.Contains(Char.ToLower(y)));
            }

            //count is returned once an element in the array is 'null'
            return (count);
        }


        //Uppercase count method
        public static int UpperCount()
        {
            int count = 0; /* count holds the amount of uppercase letters */

            //this first for loop cycles through each string that is stored in the textEntryArray List<>  
            for (int i = 0;i < Program.textEntryArray.Count; i++)
            {
                //This variable is assigned the current string that the for loop has cycled onto
                string currentString = Program.textEntryArray[i];

                //This if statement is necessary in order to prevent the compiler from failing if it reaches an element in the array that is a 'null' value
                if (Program.textEntryArray[i] == null)
                {
                    break;
                }
                //This for loop iterates through each char in the current string
                for (int x = 0; x < Program.textEntryArray[i].Length; x++)
                {
                    //An if statement is used to increment count when an uppercase letter is found
                    if (char.IsUpper(currentString[x]))
                        count++;
                }
            }

            //count is returned once an element in the array is 'null'
            return (count);
        }
        
        
        //Lowercase count method
        public static int LowerCount()
        {
            int count = 0; /* count holds the amount of lowercase letters */

            //The for loops in this method work exactly the same as the previous Uppercase method - cylcing through the strings and then cycling through the chars
            for (int i = 0; i < Program.textEntryArray.Count; i++)
            {
                //This variable is assigned the current string that the for loop has cycled onto
                string currentString = Program.textEntryArray[i];

                if (Program.textEntryArray[i] == null)
                {
                    break;
                }
                for (int x = 0; x < Program.textEntryArray[i].Length; x++)
                {
                    //An if statement is used to increment count when a lowercase letter is found
                    if (char.IsLower(currentString[x]))
                        count++;
                }
            }

            //count is returned once an element in the array is 'null'
            return (count);
        }


        //Frequency of letters Method
        public static void FreqOfLetters()
        {
            //Array to store the frequencies of each letter
            int[] freqLettersArray = new int[Convert.ToInt32(char.MaxValue)];

            // For loop iterates through each element of the textEntryArray List<> 
            for(int i = 0; i < Program.textEntryArray.Count; i++)
            {
                //This if statement is necessary in order to prevent the compiler from failing if it reaches an element in the array that is a 'null' value
                if (Program.textEntryArray[i] == null)
                {
                    break;
                }

                //This variable is assigned the current string that the for loop has cycled onto
                string currentString = Program.textEntryArray[i];

                //Converting the current string to lowercase so upper and lower case letters aren't counted seperately
                currentString = currentString.ToLower();

                // foreach statement iterates over each character.
                foreach (char c in currentString)
                {
                    // Increments the array for each letter found
                    freqLettersArray[Convert.ToInt32(c)]++;
                }
            }

            // Outputs all observed letters and their frequencies 
            for (int x = 0; x < Convert.ToInt32(char.MaxValue); x++)
            {
                if (freqLettersArray[x] > 0 && char.IsLetter((char)x))
                {
                    Console.WriteLine("\t\t\tCharacter: {0}  Frequency: {1}", (char)x, freqLettersArray[x]);
                }
            }
        }


        //Long words Method
        public static void WriteLongWords()
        {
            //Variables declared
            string[] sevenOrMoreLetterWords = new string[200]; /* String array to hold the seven letter words  */
            char[] delimiterChars = { ' ', ',', '.', '-', '/', '\'', '"' }; /* A char array containing characters than will split up the words */
            int elementCounter = 0; /* a counter that is used for the increment of the elements in the long words array */

            //this first for loop cycles through each string that is stored in the textEntryArray List<>   
            for (int i = 0; i < Program.textEntryArray.Count; i++)
            {
                //This if statement is necessary in order to prevent the compiler from failing if it reaches an element in the array that is a 'null' value
                if (Program.textEntryArray[i] == null)
                {
                    break;
                }

                //This variable is assigned the current string that the for loop has cycled onto
                string currentString = Program.textEntryArray[i];

                //String array that hold all the words once they have been split using the delimiterChars array
                string[] words = currentString.Split(delimiterChars);

                //Foreach loop to cycle through each string in the words array
                foreach (string s in words)
                {
                    //If statement to check whether the string is longer than 7
                    if (s.Length > 7)
                    {
                        //If the string is longer than 7 letters it is added to the sevenLetterWords array and the counter is incremented 
                        sevenOrMoreLetterWords[elementCounter] = s;
                        elementCounter++;
                    }
                }

                //The sevenLetterWords array is written to a file
                File.WriteAllLines(@"..\..\Text Files\Long Words Output.txt", sevenOrMoreLetterWords);
               }
           }


            //Number of Sentances from ReadAllText Method
            //The number of sentances has to be calculated differently to the manual entry - hence the use of a seperate method with different variables
            public static void NumOfSentancesReadAllText()
            {
                //the variable modifiedTextEntry contains the text from textEntry, once all of the text matches the specified expression  @"^\s*$\n|\r", ""
                //^\s*$ Removes the blank lines and \n|\r removes the specified escape sequences
                string modifiedTextEntry = Regex.Replace(Program.textEntry, @"^\s*$\n|\r", "", RegexOptions.Multiline);

                //For loop cycles through each char in resultString
                for (int i = 0; i < modifiedTextEntry.Length; i++)
                {
                    //If statement increments numOfSetances but makes sure that the period, '.', isn't just part of a URL and is being used to show the end of a sentance
                    if (modifiedTextEntry[i] == '.' && (modifiedTextEntry[i + 1] == ' ' || modifiedTextEntry[i + 1] == '\n'))
                        Program.numOfSentances++;
                }
                
                //Ouput of the number of sentances
                Console.WriteLine("\t\t\tNumber of sentances = {0}", Program.numOfSentances);
            }


        }
}
