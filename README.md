# g2CrowdRoster
G2 Crowd Roster:

Description of the project:

1)This solution is in .net Framework 4.5.2 MVC Razor version
2)All the Business functionalities that has been specified has been satisfied which are:
	* As a visitor to the site, when I vote for a team member, then the number of votes should be incremented, and the voting widget should reflect that I voted
	* As a visitor to the site, I can see how many times each listing has been voted on by other visitors
3)There are two projects in the solution:
	* One is the MVC Web project.
	* One is the testing framework project which contains all the unit tests for the specified Business Rules.
	
Persisiting Voting Info:
1) No Database is used to persist voting info.
2) In Memory Caching on the server is used to persist the caching info.
3) For persisting the info of whether the user has voted or not, we are using the browser session caching. Once you clear the browsing history its gone, but the Number of people who voted will be retained.
4) As per the requirement document, I am not stopping the voter to vote multiple times, but only letting him know that he has voted.

Running the App:
1) You need an IIS server to run the app.
2) Best IDE to be used:Visual Studio 2015.
3) Solution is not published anywhere.
