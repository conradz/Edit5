window.setTitle("Edit5");

var exit = new Command();
exit.addHandler('click', function() {
    window.exit();
});
exit.setText('Exit');

window.addApplicationCommand(exit);