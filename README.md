#_Epicodus Code Review C# Week 9 Hair Salon_

#### _Create an MVC web application for a hair salon. The owner should be able to add a list of the stylists, and for each stylist, add clients who see that stylist. The stylists work independently, so each client only belongs to a single stylist, 05.10.2019_

#### _By** Sabrina M**_


![alt text](https://github.com/sabrinamianne/HairSalon.Solution2/blob/master/Screen%20Shot%202019-07-17%20at%204.21.54%20PM.png)



## Description

_Create an MVC web application for a hair salon. The owner should be able to add a list of the stylists, and for each stylist, add clients who see that stylist. The stylists work independently, so each client only belongs to a single stylist.

## Specifications


| Behavior |
| ------------- |
| As a salon employee, I need to be able to see a list of all our stylists.|
| As an employee, I need to be able to select a stylist, see their details, and see a list of all clients that belong to that stylist.|
| As an employee, I need to add new stylists to our system when they are hired.|
| As an employee, I need to be able to add new clients to a specific stylist. I should not be able to add a client if no stylists have been added.|
As an employee, I need to be able to delete stylists (all and single).|
|As an employee, I need to be able to delete clients (all and single).|
|As an employee, I need to be able to view clients (all and single).|
|As an employee, I need to be able to edit JUST the name of a stylist. (You can choose to allow employees to edit additional properties but it is not required.)|
|As an employee, I need to be able to edit ALL of the information for a client.|
|As an employee, I need to be able to add a specialty and view all specialties that have been added.|
|As an employee, I need to be able to add a specialty to a stylist.|
|As an employee, I need to be able to click on a specialty and see all of the stylists that have that specialty.|
|As an employee, I need to see the stylist's specialties on the stylist's details page.|
|As an employee, I need to be able to add a stylist to a specialty.|

## Setup/Installation Requirements

*_Clone this repository, use this link : https://github.com/sabrinamianne/HairSalon.Solution2.git
*_Change into the work directory:: $ cd HairSalon.Solution
*_To edit the project, open it in your a text editor of your choice.
*_Start MAMP and click Open WebStart page in the MAMP window.
*_In the website you're taken to, select phpMyAdmin from the Tools dropdown.
*_Select the Import tab.
*_Unzip sabrina.mianne.sql.zip and sabrina_mianne_test.sql.zip from the project directory's top level
*_Select sabrina_mianne.sql and click Go.
*_Repeat steps 6 and 7 with the file sabrina_mianne_test.sql
*_To run the program, navigate to the HairSalon directory, then run the following commands: $ dotnet restore $ dotnet run, and open http://localhost:5000/ in your browser
*_In SQL, if you want to add table:
_ USE sabrina_mianne;
_CREATE TABLE owners (id serial PRIMARY KEY, name VARCHAR(255));
_CREATE TABLE tasks (id serial PRIMARY KEY, description VARCHAR(255));

## Known Bugs

_ No Bug related_

## Support and contact details

_Contact me by e-mail at the following address : sabrina.epicodus@gmail.com_

## Technologies Used

_C#_
_ASP.NETCore SQL_
-.NET Core App 2.2.103_


### License

*This software is licenced under the MIT licence*

**_Copyright (c) 2019 Sabrina M_**
