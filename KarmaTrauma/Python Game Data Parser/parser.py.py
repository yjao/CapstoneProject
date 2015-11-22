DATALOADER_TEMPLATE = '''
class DataLoader
{
\tpublic DataLoader()
\t{
\t\tInteractable character1 = new Interactable("name");
\t\tcharacter1.dialogue.add("hi");
\t\tgenerateMachineCode();
\t}
}'''

// Creating new Interactable
Interactable intr = new Interactable();
intr.Name = {};
intr.StrangerName = {};
intr.objectType = ObjectType.{};

// Loading in Dialogues
intr.Dialogue[{}] = "{}{}{}";

// Interactable '{}' Finished
