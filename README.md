# Kennesaw State University: SWE 4663 Software Project Management Semester Project
Here is the link to our product!
http://manageme1.us-east-1.elasticbeanstalk.com/ <br><br>

This repository contains the source code for ManageMe.io. It also contains this README that lists the product team members, includes the documents that went into the planning of this product. It also explains how we utlized AWS to host our product along with MongoDB to handle our live database. This repository also provides a quality video that further explains this repository and how. The source code of this product was completed in primary C#, HTML, and CSS with proper naming conventions and coding standards.


## Table of Contents

- [Team Members](#team-members)
- [Documentation](#documentation)
- [Functionalities of ManageMe.io](#functionalities-of-managemeio)
- [AWS](#aws)
- [MongoDB](#mongodb)
- [Final Video Presentation](#final-video-presentation)

## Team Members
**Group 4**<br>
This project was completed by the following students:
- [Maan Bhagat](https://github.com/mbsw04)
- [Matt Crowley](https://github.com/matthewcrowley)
- [Kade Fleming](https://github.com/KadeFleming)
- [Constant Nortey](https://github.com/YetronLives)
- [Caroline Varner](https://github.com/carolinevarner)

## Documentation
- [Quick Plan](dotnetTest/Artifacts/ProjectQuickPlan.pdf)
- [Comprehensive Plan](dotnetTest/Artifacts/ProjectComprehensivePlan.pdf)

## Functionalities of ManageMe.io
ManageMe.io is a system that takes managing your team and projects to the next level. It utilizes the ASP.NET Core Model-View-Controller(MVC) framework. This system is hosted on the web by AWS Elastic Beanstalk, uses MongoDB as it's database, and even works on mobile devices. ManageMe.io is a project managers wish come true!<br><br>
**Core purposes and functionalities of ManageMe.io:**<br>
- A section that the user can input the following information
	- A high-level description of the project
	- The owner or the project manager's name
	- A list of the team members assocuated with each project and task
	- A list of risks
- A project requirements section for each project
	- Allows the user to input the list of functional and nonfunctional requirements
- A method of tracking the project effort
	- Allows the user to view the amount of effort in the form of person hours expended on the projects

## AWS
ManageMe.io is deployed to the cloud. ManageMe.io utilizes .NET9 hosted on AWS Elastic Beanstalk, with CI/CD automation managed through AWS CodePipeline. This pipleine monitors the main branch of the source repository/code base, and any push to this branch triggers a build process using AWS CodeBuild. CodeBuild compiles the application and produces a deployment artifact, which is then automatically deployed to the AWS Elastic Beanstalk environment. This configuration enables efficient and consistent delivery of application updates directly from the main development branch.

## MongoDB
All data relating to our users and their projects are stored in MongoDB. We created two collections namely "Users" and "Projects" that stores user information and project information respectively.

### User Model
The user model used to store user information possessed the following attributes:

- Id (AutoGenerated by MongoDB)
- firstName
- lastName
- username
- occupation
- email
- password
- profilePhotoUrl
- createdAt

#### Registration
*firstName*, *lastName*, *username*, *email*, *occupation*, and *password* are required to create an account with the software <br>
*createdAt* keeps track of when accounts are created

#### Login
*username* or *email* together with *password* are used to login. <br>
The user has the option to upload a profile photo to their account but have a defaukt icon if not uploaded.

## Final Video Presentation
This final video demonstration of our product on YouTube covers over our repository and the product itself.<br><br>
[Click here to access our video!](https://www.youtube.com/watch?v=alnv505AxKQ)
