### BUILD
Before compiling you need to add/check references to:
* vectolic.dll
* Newtonsoft.Json.dll 

### EXECUTE
The following directories/files must be provided in the application folder (e.g. ..\bin\Release):
* vectolic.dll
* Newtonsoft.Json.dll 
* license.dat
* Decleration/
* Docs/CSE-User Manual.pdf


### RELEASE
Checklist to build a new release:
* Make  zip-folder named with the "Semantic-version", ie: 2014_15_5-VECTO_CSE-2.0.1-beta1.
* Copy into it:
    * executable (`.EXE`) (from bin/Debug when pre/beta release)
    * vectolic.dll (check for right version!! Source is currently in beta for file signing features)
    * Declaration/...
    * Docs/CSE-User Manual.pdf
    * Docs/CSE-User Manual.pdf
* Make a temp-copy of the folder and run it with a license to check everything alright.
* ZIP the original folder.
* Upload into CITNet's SVN:
    https://webgate.ec.europa.eu/CITnet/svn/VECTO/trunk/Share/
  and link from: 
    https://webgate.ec.europa.eu/CITnet/confluence/display/VECTO_CSE/Releases
* Make licenses and update private pages
* Tag repos.
* Send announcment email through JIRA (ie see VECTO-28)
