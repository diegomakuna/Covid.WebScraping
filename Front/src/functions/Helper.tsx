export default class Helper {
    static toNumberFormat(number?:number){
        
        return number ? number?.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".") : "--"
    }

    static dateToString(_date?: string){
        
           let date = new Date('2013-08-03T02:00:00Z');
           let year = date.getFullYear();
           var month: any = date.getMonth()+1;
           let dt: any = date.getDate();

            if (dt < 10) {
            dt = '0' + dt;
            }
            if (month < 10) {
            month = '0' + month;
            }
            return dt+'/' + month + '/'+ year
    }
}