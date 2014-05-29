            VECTO-CSE's Changes
            ===================
 
#### 2014-05-29: 2.0.1-pre1 ####
JRC contributions:
  * Read/write Vehicle-file as JSON.
  * prefsUI: Add Reload button.
  * Remember window-location (use .net Settings for that).
  * Start logging stack-traces in the file-log.
##### Internal:
  * Start saving stack-traces into the log-file.
  * Enhance JSON-files with standard header/body behavior.
  * Link JSON to GUI controls (labels & toolstips)
  * json: Read defaults from schemas.


#### 2014-05-23: 2.0.1-pre0 ####
JRC contributions:
  * Remove the versioning infos from app-name (manual, project-name, folders).
  * Use SemanticVersioning 2.0.0 (see http://semver.org/).
  * Possible to use any editor (not only notepad.exe).
  * Separate config/ from Declaration/ folders.
  * Added README.md, CHANGES.md, COPYING.txt files.
##### Internal:
  * Auto create config/ on the 1st run, converted to JSON with transparent error-handling.
  * FIX leaking of file-descriptors by using VB's 'Using' statement (class 'cFile_v3' now implements IDisposeable).


#### 2014-05-14: CSE2.01 ####
1st delivery from TU-Graz under Lot-3.
