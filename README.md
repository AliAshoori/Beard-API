# Beard (RESTful) APIs
This repository is an attempt to practice clean coding and tackle challenges that could happen on everyday life of developers. The main  story is to deliver a few set of RESTful APIs with respect to RESTful designs and principles. The endpoints take care of placing orders and retrieving them from a database. To find out more about technical concept please read the following. (yet to be completed)

_The codebase is still under development and new changes may be pushed to the repository sooner or later, but, the inital intention was an attemp to demonstrate clean-codign and providing different content negotiations to produce the response with or without HATEOS. And hopefully, this has been achived to some extent._

# Codebase Architecture HLA (High-level Architecture)

![BeardApiCodeBaseHLA](https://user-images.githubusercontent.com/7995157/119239458-200e2b80-bb41-11eb-994b-3e6f35dba607.PNG)

# Developer Environment and setup
To setup your development environment and playing around with the code you will need the following to installed and setup on your machine.

## Coding 
The project has been written in Visual Studio 2019, you can download the community version from [here](https://visualstudio.microsoft.com/downloads/).

## Database
Microsoft SQL Server Management studio is required and it can be downloaded from [here](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15).

## Source code 
The source code can be downloaded either as a ZIP folder or using git. To go with the latter, please download the Git from [here](https://git-scm.com/downloads). and get yourself familiar with it if not already. Alternatively, feel free to browse the files from within the Github where you are now. :-)

# API Documents
The project utilizes Swagger documentation wherein all the endpoints with their payload schemas can be explored. The screenshot below shows a snapshot of that. Also, once the project is run, the same can is available from this address: https://localhost:44350/swagger/index.html

![SwaggerForBeardApi](https://user-images.githubusercontent.com/7995157/119239712-c149b180-bb42-11eb-98f7-fe53b6bcb358.PNG)

# Testing the API manually
The endpoints can be easily tested through Postman. If you haven't got that installed already, feel free to download it from [here](https://www.postman.com/downloads/). Once you've installed the Postman, make sure you have the APIs running on your local machine.

## Testing placing a new order from Postman - no HATEOS 
1. Open up your Postamn and create a new POST request
2. For the URL put in the following address: https://localhost:44350/order
3. From Postman, click on the "Body" and choose the "raw" and from the following drop-down list select "JSON" ![PostmanExample](https://user-images.githubusercontent.com/7995157/119239831-93b13800-bb43-11eb-80f2-de29188a1282.PNG)
4. Now simply copy and paste the following request payload into the body 
`{
    "Total": 30.23,
    "CustomerId": "113d4EF0-0301-5949-8A37-FB8703420C82",
    "TransactionId": "113d4EF0-0301-5949-8A37-FB8703420C85",
    "BillingAddress": { "line1": "9", "line2": "Fishersman St", "city": "London", "country": "England", "postcode": "SE16 FFF" },
    "DeliveryAddress": { "line1": "9", "line2": "Fishersman St", "city": "London", "country": "England", "postcode": "SE16 FFF" },
    "OrderLines": [ { "unitPrice": 30.23, "quantity": 1, "productId": "15F9420F-5223-552B-BBB7-3C46F117AB8A"  } ]
}`
5. Hit enter and you should be able to receive the following response with a 201 response code
![Response](https://user-images.githubusercontent.com/7995157/119239872-fe627380-bb43-11eb-80d8-2b6eae69d2cd.PNG)

## Testing placing a new order from Postman - with HATEOS 
In order to test the same endpoint but this one with respecting to the HATEOS, follow all the above steps until number 5 and dow the following before hitting the SEND button. 
1. Click on the Headers just like the below image 
![Headers](https://user-images.githubusercontent.com/7995157/119239901-471a2c80-bb44-11eb-8e7b-a025e34e15f0.PNG)
2. In the `Accept` make sure you put in the following value: `application/wildbeard.api.hateoas+json`
3. Now send your request and this time the API should give you a different response just like the below:
![response_hateos](https://user-images.githubusercontent.com/7995157/119239946-92343f80-bb44-11eb-8d84-7f6f5f43661a.PNG)

Step number 2 has instructed the server with a new content negotiation type to return back the links as part of the response. It's always a good practice to only return these if required by the client for which a different content negotation should be implemented in the codebase. Look into the `HateosOutputFormatter` class and its references in the solution to better understand how it works.
