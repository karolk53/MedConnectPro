# MedConnectPro :health_worker: :woman_health_worker:

## What what is MedConnectPro?

It is a web application that supports work in medical offices specially the private ones. It functions not only on staff but also on patient side. 
The idea for this project came when we realised that most of daily services are subject to computerization and informatization. It concern also to the medical fields.

## Basic functionalities

Here you can some basic functionalities implemented in our project.

| Patient  | Doctor|
| ------------- | ------------- |
|  Visit registration | Add new office and its schedule |
|  Give a review for the doctor | Presentation of the price list of services offered |
| Review own vists | Visit management |
| Ask a questions for doctor | Patient overview |

## Backend

On server side main technology that we used is ASP.Net Core and created a REST API which response for handling of the most important actions in the system.
We also used Entity Framework to work with SQLite and PostgreSQL databases, what's more we have implemented cloud technology called Cloudinary to sotre images in it. 
Authentication is done by using JWT Token.

## Frontend

When it comes to frontend main techonology is Angular and TypeScript. We have connected it to our API and create GUI. 
We also added support for multiple languages. This is realized by the ngx-translate library, which is standard in Angular projects. It allows you to define translations in JSON files, making it easy to edit and extend them later. 
Each language is represented by a separate file which provides order and clarity in managing translations.

## Tests

We have done also some simple unit and integration tests of main functionalities.

## GUI

Here we can see a few screenshots of user interface.

1. Doctors list overview - Patient can review list of doctors that are already registered in the system and filter the list by choosing doctor's specialisation, enter his/her name or city where he/she works.

   <img src="https://github.com/karolk53/MedConnectPro/blob/29235a1fd8b0bb951d0b3903850a8c5683dab0e2/screenshots/list.png"  height="300px"/>

2. Schedule - Doctor can create schedule for his office by choosing day of the week and enter start time, end time and length of single visit. 

  <img src="https://github.com/karolk53/MedConnectPro/blob/29235a1fd8b0bb951d0b3903850a8c5683dab0e2/screenshots/schedule.png"  height="300px"/>

3. Calendary - when someone visit doctor's profile site they will for example see a clendary of work. Crosed out hours are taken, by clicking the available hour user will redirected to form that is responsible for register new visit.

 <img src="https://github.com/karolk53/MedConnectPro/blob/29235a1fd8b0bb951d0b3903850a8c5683dab0e2/screenshots/calendary.png"  height="300px"/>
   
## WCAG 2.1

We tried to make the application available for people with disfunctions, so that it complies with WCAG 2.1 guidlines. This was accomplished by adjusting the site's color scheme so that people with color blindness could use it comfortably and we implemented an option to change the font size.


