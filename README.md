# Cignium SearchFight 
Console application that will query against implemented search engines and will get the results for all the terms, the winners by search engine and the grand winner.

## Usage
In order to use the application, you only have to execute it from wherever you are and specify the search terms you want to look for. Search terms with spaces are also allowed! (I.E: "java script") You can also input an unlimited amount of search terms.
```
C:\Cignium.SearchFight.exe .net .java
```

```
.net: Google: 4450000000 MSN Search: 12354420
java: Google: 966000000 MSN Search: 94381485
Google winner: .net
MSN Search winner: java
Total winner: .net
```
## API Key Pre-requisites
Out of the box Cignium Search Fight implements the Google and Bing Search Engines, so you will need the following:

* Google API Key
* Google Custom Context Id
* Bing Search Engine Key

Once you have them, in order for the application to work you have update the "Cignium.SearchFight.exe.config" file. You will need the following: 

```
    <!-- Google Search Engine Settings -->    
    <add key="Google.ApiKey" value="ADD_YOUR_GOOGLE_API_KEY_HERE" />
    <add key="Google.ContextId" value="ADD_YOUR_GOOGLE_CONTEXT_ID_HERE" />

    <!-- Bing Search Engine Settings -->    
    <add key="Bing.ApiKey" value="ADD_YOUR_BING_API_KEY_HERE" />
```

## Customization
There are some things that Cignum SearchFight allows you to customize, if you want:
* You can add another Search Engine, just create a new class that implements the ISearchEngine interface.
* You can customize several parts of the application. See SearchFightKernel, and replace any default Manager implementation with your own:
  * SearchManager controls the way we do the searches for the Search Engines.
  * WinnerManager controls the way we calculate search engine winners and grand winners
  * ReportManager controls the way we show the results.

## Deployment
Compile the solution with visual studio and the exe will be generated. You can also get a pre-compiled copy from the Releases! (You still need to update your configuration!)