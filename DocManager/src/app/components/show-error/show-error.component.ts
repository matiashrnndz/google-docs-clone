export class ShowErrorComponent {

    errorMessage:string ="";
   
    handleError(error: any): void {
        this.errorMessage = JSON.stringify(error).replace(/"/gi, '');
      }
}