VECTO-CSE: Development
======================

### BUILD
Before compiling you need to add/check references to:

  * vectolic.dll
  * Newtonsoft.Json.dll 

### EXECUTE
The following directories/files must be provided in the application folder (e.g. ..\bin\Debug):

  * vectolic.dll (check correct version)
  * Newtonsoft.Json.dll 6.0.0
  * license.dat
  * Declaration/
  * DemoData/
  * Docs/CSE-User Manual.pdf (generated from Word-file)


### RELEASE

Checklist to build a new release:

  1. Make  zip-folder named with the "Semantic-version", ie: 2014_15_5-VECTO_CSE-2.0.1-beta1.
  2. Copy into it:
      * executable (`.EXE` and optionally `.PDB` when a prerelease)
      * vectolic.dll (check correct version)
      * Newtonsoft.Json.dll 6.0.0
      * Declaration/ (With all its files marked as READONLY!!)
      * DemoData/
      * Docs/CSE-User Manual.pdf (*remember* to generate from Word-file and delete irrelevant files)
      * Docs/GenericData.xlsx
      * README.md
      * CHANGES.md
      * COPYING.txt
  3. Make a temp-copy of the folder and run it with a license.
      * Does the "User Manual" open?
  4. Check everything alright, or else go back to step (2).
  5. Issue a Pull-request to CITNet with all latest changes.
  6. ZIP the original folder.
  7. Upload into CITNet's SVN:
      https://webgate.ec.europa.eu/CITnet/svn/VECTO/trunk/Share/
    and link from: 
      https://webgate.ec.europa.eu/CITnet/confluence/display/VECTO_CSE/Releases
  8. Make licenses and update private pages
  9. Tag repos.
  10. Send announcment email through JIRA (ie see VECTO-28).
