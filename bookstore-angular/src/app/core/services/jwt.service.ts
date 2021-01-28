import { Injectable } from "@angular/core";
import { AppSettings } from '../common';

@Injectable({
    providedIn: 'root'
})
export class JwtService {

    getToken(): String {
        return window.localStorage[AppSettings.JwtToken];
    }

    saveToken(token: String){
        window.localStorage[AppSettings.JwtToken] = token;
    }

    destroyToken(){
        window.localStorage.removeItem(AppSettings.JwtToken);
    }

}