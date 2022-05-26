#  Welcome to Introduction.CodingAssignment!

## Instructions
- Clone the Repo in Visual Studio or your selected compatible IDE
- Run the project in the debugger in Visual Studio OR 
- Build the project for release and navigate to the build output folder - for Visual Studio: *\Introduction.CodingAssignment\src\Introduction.CodingAssignment\bin\Release\net6.0  
- Find the executable file named Introduction.CodingAssignment.exe and run it by double clicking on the file.
- You will be prompted to either add your own data by typing the path to the file you want to load or to use the default file (provided in the specification)
- A window will open and the data will be processed and displayed the result on the console window

## Some things about the application 
- Some concerns about extracting and formatting raw data from the input files.
	- Since getting the data includes an I/O action, this is done asynchronously and cached for better performance.
	- We have some modeling that is needed to structure the data, and so need a module that can extract the raw data and transform it into a model that can be used for processing. 
- In order to remain light we include singleton bindings in the container

## Running unit tests. 
- Open the project in Visual Studio and Run all tests (Cntrl + R, T)
	- Test that we can collect the test data, 
	- That that we transform it into a usable form
	- Make sure that the application can process the data and that the output is correct
	- Test that we can run the processor with data and that it doesn't fail.

