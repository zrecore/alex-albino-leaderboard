import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Record } from '../Models/record';
import { Observable } from 'rxjs/Rx';

// Import RxJs required methods
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class RecordService {
    // Resolve HTTP using the constructor
    constructor (private http: Http) {}

    // Private instance variable to hold the base URL
    private endpointURL = 'http://localhost:5000/api/records';

    // Fetch existing records...
    fetch(params:any) : Observable<Record[]> {
        // ...using GET request
        return this.http.get(this.endpointURL, {search: params})
            // ...and calling .json() on the response to return data
            .map((res:Response) => res.json())
            // ...handle any errors
            .catch((error:any) => Observable.throw(error.json().error || 'Server Error!'));
    }

    // Find existing record by query (ID for now)...
    find(id:String) : Observable<Record[]> {
        // ...using GET request
        return this.http.get(`${this.endpointURL}/${id}`)
            // ...and calling .json() on the response to return data
            .map((res:Response) => res.json())
            // ...handle any errors
            .catch((error:any) => Observable.throw(error.json().error || 'Server Error!'));
    }

    // Add a new record
    add (body: Object): Observable<Record[]> {
        let bodyString = JSON.stringify(body); // Stringify the payload
        let headers = new Headers({ 'Content-Type': 'application/json' }); // Set the content-type as JSON
        let options = new RequestOptions({ headers: headers }); // Create a request option

        return this.http.post(this.endpointURL, body, options)
                .map((res:Response) => res.json())
                .catch((error:any) => Observable.throw(error.json().error || 'Server Error!'));
    }

    // Update an existing record
    update (body: Object): Observable<Record[]> {
        let bodyString = JSON.stringify(body);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.put(`${this.endpointURL}/${body['ID']}`, body, options)
                .map((res:Response) => res.json())
                .catch((error:any) => Observable.throw(error.json().error || 'Server Error!'));
    }

    // Delete a record (Note, this really should be protected from free and unfettered access)
    remove (id:String): Observable<Record[]> {
        return this.http.delete(`${this.endpointURL}/${id}`)
            .map((res:Response) => res.json())
            .catch((error:any) => Observable.throw(error.json().error || 'Server Error!'));
    }

}
