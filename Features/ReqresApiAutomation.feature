Feature: ReqresApiAutomation

Specflow API automation for Reqres List User, List Single User, Create User, Put User and Update User endpoint

Scenario Outline: The user sends a request to a valid Reqres endpoint
	Given the user sends a <http method> request with url as Reqres <uri>
	Then the request should return <status code> status code
	And the response message for the <http method> request is as expected

	Examples: 
	| http method | uri     | status code |
	| GET         | users   | OK          |
	| GET USER    | users/1 | OK          |
	| POST        | users   | OK          |
	| PUT         | users/2 | OK          |
	| PATCH       | users/3 | OK          |

Scenario Outline: The user sends a request to an invalid Reqres endpoint
	Given the user sends a <http method> request with url as Reqres <uri>
	Then the request should return <status code> status code

	Examples: 
	| http method | uri     | status code |
	| GET         | xuser   | NotFound    |
	| GET         | xuser/1 | NotFound    |
	| POST        | xuser   | NotFound    |
	| PUT         | xuser/2 | NotFound    |
	| PATCH       | xuser/3 | NotFound    |