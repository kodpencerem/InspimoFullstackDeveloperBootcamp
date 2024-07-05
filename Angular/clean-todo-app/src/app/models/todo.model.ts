export class TodoModel {
    id: string = "";
    work: string = "";
    note?: string | null = null;
    deadLine: string = "";
    isCompleted: boolean = false;
    isShowUpdateForm: boolean = false;
}