window.setTitle("Edit5");

var exit = new Command();
exit.addHandler('click', function() {
    window.exit();
});
exit.setText('Exit');

window.addApplicationCommand(exit);

var newFile = new Command();
newFile.addHandler('click', function() {
    var edit = window.editors.newEditor();
    edit.setText("New Editor");
    edit.setTitle("Untitled");
});
newFile.setText('New');

window.addApplicationCommand(newFile);