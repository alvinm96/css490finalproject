import { Injectable } from '@angular/core';

@Injectable()
export class ResultsService {
  results: ImageModel[] = [];

  resetResults() {
    this.results = [];
  }

  addResult(obj: any) {
    this.results.push(obj);
  }

}

class ImageModel {
  groupName: string;
  userName: string;
  imageDescription: string;
  imageObj: any;
}