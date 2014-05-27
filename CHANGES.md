            VECTO-CSE's Changes
            ===================

### 2014-05-23: CSE-2.0.1-pre0

* Remove the versioning infos from app-name (manual, project-name, folders).
* Possible to use any editor (not only notepad.exe).
* Separate config/ from Declaration/ folders.
* Added README.md, CHANGES.md, COPYING.txt files.
#### Internal:
* Auto create config/ on the 1st run, convert it to JSON with transparent error-handling.
* FIX leaking of file-descriptors by using VB's 'Using' statement (class 'cFile_v3' now implements IDisposeable).
