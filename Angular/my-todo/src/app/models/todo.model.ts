export class TodoModel {
    id: string | undefined;
    work: string = "";
    isCompleted: boolean = false;
    createdAt: Date = new Date();
}