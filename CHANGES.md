VECTO-CSE: Changes
===================


TODO: 2014-06-??: v2.0.1
-----------------
  * JSON-ize preferences, vehicle-file, job-file, criteria-file
  * Provide default-values and help-messages in GUI/files with infos from "schemas".
  * Start improving error-reporting by including stack-traces and timestamps into the log-file, for post-mortem examination.
  * Separate config/ from Declaration/ folders.
  * Standarize versinong using [SemanticVersioning](http://semver.org/).
  * Possible to use any editor (not only notepad.exe) for viewing files.
  * Welcome developers with README.md, CHANGES.md and COPYING.txt files.
##### Internal:
  * Implement an API for writing Header/Body json-files.
  * Apply Object-oriented design weith resource-management when I/O files.
  * Improve logging-API so now a single log-routine is used everywhere(instead of 3 different ones).
  * General restructuring of the folders and names in the project.

More analytically:



#### 2014-06-03: v2.0.1-pre1 ####
JRC contributions:

  * Read/write Vehicle-file as JSON.
  * prefsUI: Add Reload button.
  * Remember window-location (use .net Settings for that).
  * All logs (even those sent to msg-box) are written to log-file, with timestamps and stack-traces.
##### Internal:
  * Start saving stack-traces into the log-file.
  * Enhance JSON-files with standard header/body behavior.
  * Link JSON to GUI controls (labels & toolstips)
  * json: Read defaults from schemas.
  * Rework logging as a single routine, whether invoked from Background Worker or not.


#### 2014-05-23: v2.0.1-pre0 ####
JRC contributions:

  * Separate config/ from Declaration/ folders.
  * Remove the versioning infos from app-name (manual, project-name, folders) and 
    use [SemanticVersioning](http://semver.org/) 2.0.0 instead.
  * Possible to use any editor (not only notepad.exe).
  * Added README.md, CHANGES.md, COPYING.txt files.
##### Internal:
  * Auto create config/ on the 1st run, converted to JSON with transparent error-handling.
  * FIX leaking of file-descriptors by using VB's 'Using' statement (class 'cFile_v3' now implements IDisposeable).


#### 2014-05-14: CSE2.01 ####   
1st delivery from TU-Graz under Lot-3.
