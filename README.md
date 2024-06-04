# Building and Securing Restful APIs in .NET
This is the repository for the LinkedIn Learning course Building and Securing Restful APIs in .NET. The full course is available from [LinkedIn Learning][lil-course-url].

![lil-thumbnail-url]

Most people have heard of REST APIs, but the underlying concept—representational state transfer (REST)—can still cause a good deal of confusion. RESTful APIs use REST architecture along with HTTP requests to transfer data and changes in application state between clients and servers. This course shows you how to apply the principles of REST while building secure RESTful APIs on top of ASP.NET.

Join instructor Matt Milner as he provides an overview of how to get up and running with RESTful design in ASP.NET. Learn how to create APIs, entities, and databases, as well as work with resources, add link support, and configure and enable authentication options. By the end of the course, you should know the basics—how to properly request and return data in ASP.NET—and the best practices for building secure and scalable APIs to serve web clients, mobile clients, and beyond.

_See the readme file in the main branch for updated instructions and information._
## Instructions
This repository has branches for each of the videos in the course that shows code examples. You can use the branch pop up menu in github to switch to a specific branch and take a look at the course at that stage, or you can add `/tree/BRANCH_NAME` to the URL to go to the branch you want to access.

## Branches
The branches are structured to correspond to the videos in the course. The naming convention is `CHAPTER#_MOVIE#`. As an example, the branch named `02_03` corresponds to the second chapter and the third video in that chapter. 
Some branches will have a beginning and an end state. These are marked with the letters `b` for "beginning" and `e` for "end". The `b` branch contains the code as it is at the beginning of the movie. The `e` branch contains the code as it is at the end of the movie. The `main` branch holds the final state of the code when in the course.

When switching from one exercise files branch to the next after making changes to the files, you may get a message like this:

    error: Your local changes to the following files would be overwritten by checkout:        [files]
    Please commit your changes or stash them before you switch branches.
    Aborting

To resolve this issue:
	
    Add changes to git using this command: git add .
	Commit changes using this command: git commit -m "some message"

## Installing
1. To use these exercise files, you must have the following installed:
	- [Visual Studio Code][vscode-url]
    - [.NET 8 SDK][net8sdk-url]
2. Clone this repository into your local machine using the terminal (Mac), CMD (Windows), or a GUI tool like SourceTree.


### Instructor

Matt Milner

Independent Consultant, Web Developer, Trainer

                            

Check out my other courses on [LinkedIn Learning](https://www.linkedin.com/learning/instructors/matt-milner?u=104).


[0]: # (Replace these placeholder URLs with actual course URLs)

[lil-course-url]: https://www.linkedin.com/learning/building-and-securing-restful-apis-in-dot-net
[lil-thumbnail-url]: https://media.licdn.com/dms/image/D4E0DAQHYuErIMEOjXw/learning-public-crop_675_1200/0/1716919545186?e=2147483647&v=beta&t=hOU5JJxa_EBLxQwnNg13Qoio-oAYuo1fHcDShgXa9SA
[vscode-url]: https://code.visualstudio.com/
[net8sdk-url]: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

