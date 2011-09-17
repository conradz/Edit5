window.setTitle("Edit5");

var cmd = window.commands;
var exit = new Command();
exit.setText('Exit');
exit.addHandler('click', function() {
    window.exit();
});

cmd.addApplicationCommand(exit);