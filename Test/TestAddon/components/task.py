def TaskAction(output):
	output("Hello, world!!!")

AndromedaApi.Register({
	"type": "Task",
	"name": "TestTask",
	"action": TaskAction
})