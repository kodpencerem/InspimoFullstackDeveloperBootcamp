export class UserModel {
    id: string = "";
    firstName: string = "";
    lastName: string = "";
    fullName: string = "";
    userName: string = "";
    password: string = "";
    file?: any;
    avatar: string = "";
    profession: string = "";
    isActive: boolean = false;
    lastActiveDate?: string | null = null;
    isActiveInformation: string = "It was last active 10 minutes ago"; // Right now they here!
}