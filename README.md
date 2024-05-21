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

1. 

## WCAG 2.1