## Loranzatto's Append

System Specification
OS: Windows Pro 10 64 bit
Microsoft .NET Framework: 4.7
VS 2017 Version: 15.7.6

I've cloned whole tech-interview project and made an adapt in it to be included in a C# .NET Console Application.

Now in this repository we have a C# .NET Console Application solution (.sln) with two projects:
- tech-interview;
- tech-interview_TestProject;

Steps to Execute our Console Application:

By Generated Exe
1. After clone this repository to your computer go to the bin folder (tech-interview\tech-interview\bin\Debug);
2. Change the file path that should be read and write JSON Files in ConsoleApplication.exe.config to one in your own computer;
3. Execute ConsoleApplication.exe file and the logic will be processed:
   - Read the data.json;
   - Execute the proposed logic;
   - Write the results into the output.json;
4. The logic will update the already existing output.json file within each level folder (backend -> Level1 , Level2 and Level3);

By VS 2017 IDE
1. After clone this repository to your computer open the solution (.sln) in VS 2017 IDE;
2. Change the file path that should be read and write JSON Files in App.config to one in your own computer;
3. Clean and Rebuild the solution;
4. Start the application execution and the steps detailed in the topic above (3 and 4) will be the same;

Steps to Execute our Unit Test Application:

1. After clone this repository to your computer open the solution (.sln) in VS 2017 IDE;
2. Go to the Test -> Run -> All Tests and the test project will be executed;
3. Check the resu;ts at the unit test tab view;
