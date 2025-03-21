# Lab 1
# Completed by Makar Spetsyian, group 353504

## Functional requirements
 The program must be able to:

- draw 3 types of figures: rectangle, triangle, ellipse
- erase an object
- move object
- add a background to a figure
- save canvas as a file
- load from file
- undo/redo action

  Warning: all numeric input values must be integers. If something goes wrong error will be shown in the screen. 
  Objects may be displayed with altered proportions due to the peculiarities of console output.

  
  Size of canvas: 200x40
  ### Menu

* _0_ - exit (without saving)
* _1_ - choose figure for adding
    * _1_ - rectangle
    * _2_ - triangle
    * _3_ - ellipse
* _2_ - object seletction menu
    * _i_ - info
    * _p_ - previous object
    * _n_ - next object
    * _m_ - move
    * _e_ - erase
    * _b_ - change background
    * _0_ - exit     
* _3_ - save canvas
* _4_ - load canvas
* _5_ - undo 
* _6_ - redo

### Choose figure for adding
U should choose a figure and required parameters, then it will be added to canvas and will be drawn automatically.

### Object selection menu
U can see info about selected figure(defalt pointer to last added), choose any figure by iteration(p or n button), all actions are performed on the selected figure.

### Save
Enter a filename to save canvas as file

### Load
Enter a filename to load canvas from file

### Undo 
Undo action on canvas

### Redo 
Redo action on canvas
