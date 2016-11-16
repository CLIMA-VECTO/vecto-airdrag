VECTO-CSE: Changes
===================
#### 2016-11-16: v3.0.0 ####
TUG improvements:

  * New version number
  * Update of needed signals in calibration run (t_ground no longer needed as input)
  * Always a 3 month lisense will be delivered with the program
  * Update DEMOData

#### 2016-11-09: v2.0.7-beta8/9 ####
TUG improvements:

  * Correction of rolling resistance criteria
  * Correction of digit test for coordinates
  * Update of needed signals in calibration run (t_ground no longer needed as input)

#### 2016-10-20: v2.0.7-beta7 ####
TUG improvements:

  * New version number
  * Including Declaration and Engineering Mode to program and Job-File
  * Fv_veh calculation update (now option 2 is the new preferred one) Option 2 is deleted from result file
  * Uneven numbers of datasets per heading in HS test now allowed
  * Calculation of F_acc changed due to wheels inertia deletion (now F_acc = 1.03*m*a)
  * New design of criteria tab
  * Update Standard criteria values + renaming
  * Include of new variables for validity criteria (t_amb; t_ground; tq_grd)
  * gradient correction implemented
  * Check of digits after decimal separator for all coordinates, transmission ratios (gear + axle) and altitudes (Values in vehicle file changed to string values)
  * Vehicle file check included (For height and class code)
  * Expand vehicle file with vVehMax and GVMMax value
  * Change vehicle file configuration parameter from "Rigid/Tractor" to "No/Yes"
  * Displacement of Genshape file into code
  * Add reference vehicle high in genshape class
  * Add minimum/maximum vehicle high in genshape class
  * Control of min/max height with vehicle height only in Declaration mode
  * Calculation of new Result values (delta_CdxA_height; v_avg_LS/HS; t_amb_LS1, CdxA(ß)_H1/2, beta_H1/2)
  * Change of CdxA(ß) and beta calculation
  * Output files in Declaration mode extended
  * Output files adapted
  * Expansion of the job-File due to new results
  * vehWidth and wheelsInertia deleted from vehicle-file
  * Deletion of unused variables (omega_wh; omega_wh_acc; omega_p_wh_acc; t_tire; p_tire; ...)
  * Deletion of [ss.ss] coordinate input
  * Update DEMOData
  * Update of Excel DemoData file
  * Update user manual and release notes

#### 2016-01-21: v2.0.6-beta6 ####
TUG improvements:

  * New version number
  * Bugfix control heading calculation in *.csms file
  * Bugfix old functions for day changes deleted
  * Update user manual and release notes
  
#### 2015-11-25: v2.0.5-beta6 ####
TUG improvements:

  * New version number
  * Renaiming of VECTO-CSE into VECTO-Air Drag
  * Bugfix dist calculation
  * Bugfix calculation of a_veh_avg and omega_p_wh_acc
  * Update of the ending message if an error during the calculation detected
  * Update user manual and release notes
  
#### 2015-10-13: v2.0.4-beta6 ####
TUG improvements:

  * New version number
  * Updates by error/warning output
  * Update user manual and release notes
  * Free definition for list and decimal separators in the CSV-Files
  * Variable definition of the coordinate unit (*.csms and *.csdat files)
  * Update of the excel file 
  * Update error messages
  
#### 2015-07-23: v2.0.3-beta6 ####
TUG improvements:

  * New version number
  * Bugfix calculation without additional signals
  * Update user manual and release notes (Now always the newest version will be opened)
  * Deleted unused variables (FC from input data, t_amb_tamac from criteria)
  * Add ground temperature as optional signal to input data (<t_ground>)
  * Add new criteria parameter t_ground_max = 40°C
  * Set t_amb_max to 25°C (old 35°C)
  
#### 2015-07-20: v2.0.2-beta6 ####
TUG improvements:

  * Bugfix heading control
  * Update user manual
  * Add "Report bug" option
  * Add JIRA Quick Start Guide
  * Delete valid_t_tire from output files and job-file
  * Change t_tire from required input data to optional (like p_tire)
  * Delete rho_air_ref from criteria and GUI
  
#### 2015-07-01: v2.0.2-beta6 ####
TUG improvements:

  * New Version number.
  * Update region and language settings: now it is regardless of the system which settings are used ! But the file definitions are still the same
  * Update of the excel makro in terms of the system region and language settings
  
#### 2015-06-24: v2.0.2-beta5 ####
Mostly TUG improvements:

  * FIX Job-save missing criteria (hack)
  * Update of calculation of the CdxA value from measured drag forces
  * Update GenShape File
  * Update of calibration of vehicle speed and anemometer speed (high speed test instead of "claibration test")
  * new criteria delta_n_eng for LS/HS included
  * Option in VECTO-CSE to read in cardan speed instead of engine speed and gear ratios for HS and LS
  --> Update of the vehicle file
  * Update calculation with cardan speed: Calculation only if engine speed is not given an automated transmission is used
  * Program user friendly updates
  * Update F_acc calculation
  * Update r_dyn calculation for each test (HS, LS1, LS2)
  * Update DemoData
  * Update user manual and release notes
  * Output folder will be now created automatically if it´s not existing without questioning
  * Welcome form added for first application start
  * Correction of welcome window
  * Add the Excel-makro to generate the input data into the DemoData files
  * New Version number.
  
#### 2015-05-21: v2.0.1-beta5 ####
TUG improvements:

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
TUG improvements:

  * Bugfix linear regression calculation LS
  * Add control for beta angle <= 360° and calculation to +-180° if angle > +-180°
  * Update user manual
  * New Version number.
  
#### 2014-11-10: v2.0.1-beta3 ####
TUG improvements:

  * Correction HS calculation
  * Correction length calculation for the first section in the dat-file when there is a gap between the first and second MS.
  * Update length calculation in csms-file to check if specified length from user and coordinates are the same
  * New Version number.
  
#### 2014-09-30: v2.0.1-beta2 ####
TUG improvements:

  * Correction GenShape Check
  * New Version number.

#### 2014-09-15: v2.0.1-beta1 ####
TUG improvements:

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
