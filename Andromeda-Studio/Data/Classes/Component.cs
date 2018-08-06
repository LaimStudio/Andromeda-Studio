using AndromedaStudio.Components;

namespace AndromedaStudio.Classes
{
    public class Component
    {
        public string Name;
        public string Type;

        public ProjectTemplate AsProjectTemplate() => (ProjectTemplate)this;
        public Task AsTask() => (Task)this;
    }
}