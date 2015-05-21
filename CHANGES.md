VECTO-CSE: Changes
===================
#### 2014-11-13: v2.0.1-beta5 ####
Mostly TUG improvements:

  * Handling of time steps where coordinates are constant over a certain time period (GPS accuracy issue) set gradient to 0 to avoid division by zero 
  * Allow also non-continuous input data in *.csdat files
  * Direct start option included 
  * Anemometer instrument calibration removed from CSE calculation 
  * Update of definition of beta-signal (180° = air flow from front)
  * Acceleration correction: true as default
  * Control if heading and given direction is always the same 
  * set criteria to "standard" when CSE opens 
  * store information when switching between tabs 
  * ResetCriteria and ImportCriteria do not clear main tab
  * New Version number.
  
#### 2014-11-13: v2.0.1-beta4 ####
Mostly TUG improvements:

  * Bugfix linear regression calculation LS
  * Add control for beta angle <= 360° and calculation to +-180° if angle > +-180°
  * Update user manual
  * New Version number.
  
#### 2014-11-10: v2.0.1-beta3 ####
Mostly TUG improvements:

  * Correction HS calculation
  * Correction length calculation for the first section in the dat-file when there is a gap between the first and second MS.
  * Update length calculation in csms-file to check if specified length from user and coordinates are the same
  * New Version number.
  
#### 2014-09-30: v2.0.1-beta2 ####
Mostly TUG improvements:

  * Correction GenShape Check
  * New Version number.

#### 2014-09-15: v2.0.1-beta1 ####
Mostly TUG improvements:

  * IO: Data reading corrected for first data set in measurement section file, weather file and data files.
  * New Version number.

TODO: 2014-06-25: v2.0.1
--------------------
Mostly JRC contributions (see VECTO-29 & VECTO-35):

  * IO: JSON-ize preferences, vehicle, job & criteria-files EXCEPT from Track-file.
  * IO: CSVize all the rest files with a single header line and use '#' for comment lines.
  * IO: Separate config/ from Declaration/ folders.
  * UI: Provide default-values and help-messages in GUI/files with infos fetched from JSON-schemas.
  * UI: Make the Log-window visible at all times (more necessary now that unhandled exceptions are appropriately reported).
  * Log: Gather all unhandled exceptions and report them into log-window and log-file.
  * Log: Improving error-reporting by including stack-traces and timestamps into the log-file, for post-mortem examination.
  * Translate all file-paths against `Prefs.WorkingDir`, so that i.e. Job-files can be ported to other computers.
  * Possible to specify any editor (not only notepad.exe) for viewing files.
  * Standarize versioning using [SemanticVersioning](http://semver.org/).
  * Welcome developers and users with README.md, CHANGES.md and COPYING.txt files.
##### Internal:
  * Implement an API for writing Header/Body json-files.
  * Apply Object-oriented design weith resource-management when I/O files.
  * Sporadic fixes to work with filenames having 2-part extensions (ie `some_file.csjob.json`).
  * Log: Improve logging-API so now a single log-routine is used everywhere(instead of 3 different ones).
  * async: Stop abusing worker-Thread with Globals, use DoWorkEventArgs instead.
  * async: Start using Exceptions instead of CancelAsync() and error-flags.
  * General restructuring of the folders and names in the project.



More analytically:

#### 2014-06-23: v2.0.1-beta0 ####
Mostly TUG improvements:

  * json: Store run-results within the Job-file.
  * csv: Ensure result-files are valid CSVs.
  * csv: Changed comment symbol in CSV files from 'c' --> '#'
  * csv: Unify hunits into header labels.
##### Internal:
  * Use Exceptions instead of CancelAsync() and error-flags in calc-routines and input.vb.
  * Remove unused distVincenty() func.
  * Added EUPL preamble on all source-files.


#### 2014-06-04: v2.0.1-pre2 ####
JRC contributions:

  * Convert Job & Criteria files to JSON and possible to store them separately.
  * Still supporting old format for reading.
  * Use Use WorkingDir trick for all job-file paths, so that Job-files can be ported to other computers.
  * UI: Make the Log-window visible at all times (more necessary now that unhandled exceptions are appropriately reported).
  * UI: Setup criteria-infobox from JSON-schema.
##### Internal:
  * Log unhandled exceptions.
  * Gather all infos related to Job-properties (type, description, units) in a single place, the JSON-schema for the job-file.
  * async: Stop abusing worker-Thread with Globals, use DoWorkEventArgs instead.
  * Sporadic fixes to work with filenames having 2-part extensions (ie `some_file.csjob.json`).
  * More refactorings to simplify structure of source files and folders.


#### 2014-05-30: v2.0.1-pre1 ####
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
