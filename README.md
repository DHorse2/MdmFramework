"# MdmFramework" 
Being prepared for distribution (public)

## The Framework
A scholastic and proprietary journey duplicating a few well understood patterns and solutions within the .Net C# world. What this project assumed:
* I'm aware many good frameworks exist. The purpose was educational first and foremost.
* Database conversion tools exist too or might be built in. Databases typically can import data from disparate environments (assuming the source has an export feature). Powerful ETL might justify having this.
* In addition, broad duplication of OS classes within an API **is an antipattern**. It almost always is although there are specific use cases like wrappers. Again this is a "lesson".
* An Application Layers abstraction immitating the Network Abstraction Layers is a discussion... Somewhat an academic topic, really interesting and not in demand.
* The MVVC product is structured is built using these classes. Overtop of them and another, higher level.
* The database import product is built using the MVVC. It's turtles all the way down.
* Finally, the clipboard is defined at a lower level but still does database IO.
* I should find a diagram I have and include it here. todo

## Mdm Win Link Tool
You will notice many links everywhere. It show the presence of, and widespread usage of Windows shortcuts (links).
* Unless deleted they automagically provide the test data needed for the Win Link Utility.
* After, they provide a great UIX.
* Also, an example for Windows Idomatice Usage Guidelines.
* Regarless, I am aware they introduce a pain amount of paging when viewing the folders in an IDE.

## Mdm Clipboard (C#)
Mentioned here only because it was first built on using the framework. It was easier and faster at the time.

## Mdm Import Utility (C#)
Transfers Schemas and Data between widely disparate platforms (PICK and SQL variants). An ETL (extract, transform, load) tool.

## Mdm SRT Tools (search, replace, transform)
Under review. Proprietary.
