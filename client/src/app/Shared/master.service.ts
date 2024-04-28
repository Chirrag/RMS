import { ChangeDetectorRef, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Login } from './models/login.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Search } from './models/search.model';
import { ActivatedRoute, Router } from '@angular/router';
import { Report } from './models/report.model';
import { Userinfo } from './models/userinfo.model';
import { PasswordChange } from './models/passwordChange.model';
import { ReportSource } from './models/reportSource.model';
import { ReportType } from './models/reportType.model';
import { CreateReport } from './models/createReport.model';
import { ReportData } from './models/applicationTypes/reportData.model';
import { StudyData } from './models/applicationTypes/studyData.model';
import { TargetData } from './models/applicationTypes/targetData.model';
import { PostReportData } from './models/PostReportData';
import { SearchView } from './models/SearchView.model';
import { UpdateModel } from './models/applicationTypes/update.model';
import { state } from '@angular/animations';


@Injectable({
  providedIn: 'root'
})
export class MasterService {
  constructor(private http: HttpClient, private route: ActivatedRoute,private router: Router) { }
  routeName: string = "";
  searchLoader: boolean = false;
  readonly baseUrl = environment.apiUrl;
  searchResults: Search[] = [];
  errorMessage:any;
  //LOGIN
  postLoginUser(data: Login): Observable<Login> {
    return this.http.post<Login>(this.baseUrl + "/Authenticate/login", data);
  }


  //SEARCH RESULTS
  getSearchResult(input: string): Observable<Search[]> {
    const encryptedInput = this.encryptSearch(input);
    this.router.navigate(['/search'], { queryParams: { parameters: encryptedInput }, replaceUrl: true });
    return this.http.post<Search[]>(`${this.baseUrl}/Home/Search`, { searchData: encryptedInput });
  }

  onSearch() {
    this.searchResults = [];
    this.searchLoader = true;
    this.getSearchResult(this.route.snapshot.queryParams['parameters']).subscribe(
      res => {
        // console.log(res);
      //  this.SearchParameter=this.route.snapshot.queryParams['parameters'];
      //  console.log(this.SearchParameter)
        this.searchResults = res as Search[];
        this.searchLoader = false;
      },
      err => {
        console.log(err);
        this.searchLoader = false;
      }
    );
  }

  // onSearch() {
  //   const searchInput = this.route.snapshot.queryParams['parameters'];
  //   // Check if the search input is not empty
  //   if (searchInput && searchInput.trim() !== '') {
  //     this.searchResults = [];
  //     this.searchLoader = true;
  
  //     // Call the API only if the search input is not empty
  //     this.getSearchResult(searchInput).subscribe(
  //       res => {
  //         this.searchResults = res as Search[];
  //         this.searchLoader = false;
  //       },
  //       err => {
  //         console.log(err);
  //         this.errorMessage = 'Error fetching search results. Please try again.';
  //         this.searchLoader = false;
  //       }
  //     );
  //   } else {
  //     // If the search input is empty, display an error message
  //     this.errorMessage = 'Please enter a valid search value.';
  //   }
  // }
  
  

  encryptSearch(searchData: string): string {
    const encryptedData = searchData.split('').map(char => String.fromCharCode(char.charCodeAt(0) + 1)).join('');
    return encryptedData;
  }

  getSearchViewDetails(searchString: string, studytype: string): Observable<SearchView[]> {
    const encryptedSearchName = this.encryptSearch(searchString);
    const encryptedStudyType = this.encryptSearch(studytype);
    this.router.navigate(['/reportDetails'], {
      queryParams: { parameters: encryptedSearchName, studyType: encryptedStudyType},
      replaceUrl: true
    })
    return this.http.get<SearchView[]>(`${this.baseUrl}/Home/SearchDetail?SearchName=${encryptedSearchName}&studyType=${encryptedStudyType}`);
  }

  //GET USER INFO
  getUserInfo(): Observable<Userinfo> {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,
    });

    return this.http.get<Userinfo>(this.baseUrl + "/User/GetUserInfo", { headers: header });
  }

  //UPDATE USER INFO
  postUpdateUserInfo(data: Userinfo): Observable<Userinfo> {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,
    });

    return this.http.post<Userinfo>(this.baseUrl + "/User/EditProfile", data, { headers: header });
  }

  //CHANGE PASSWORD
  postChangePassword(data: PasswordChange): Observable<PasswordChange> {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,
    });

    return this.http.post<PasswordChange>(this.baseUrl + "/User/ChangePassword", data, { headers: header });
  }

  //UPDATE PROFILE PICTURE
  postChangeProfilePicture(file: string): Observable<any> {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,
    });

    return this.http.post<any>(this.baseUrl + "/User/SetUserImage", { binaryData: file }, { headers: header });
  }


  //GET REPORTS
  getAllReports(): Observable<Report[]> {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,
    });

    return this.http.get<Report[]>(this.baseUrl + "/Report/GetReports", { headers: header });
  }

  //DELETE REPORT
  postDeleteReport(recID: number): Observable<number> {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,
    });

    return this.http.delete<number>(this.baseUrl + `/Report/Delete/Report/${recID}`, { headers: header });
  }


  //GET ALL ZIP-FILES
  getAllZipFiles(): Observable<string[]> {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,
    });

    return this.http.get<string[]>(this.baseUrl + "/Report/GetZipFiles", { headers: header });
  }

  ZipDownloadFile(data: string) {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;
    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,
    });

    return this.http.get(this.baseUrl + "/Report/GetZipFilesData/" + data, { headers: header, responseType: 'blob' });
  }

  //CREATE REPORT APIs
  getReportSource(): Observable<ReportSource[]> {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,
    });

    return this.http.get<ReportSource[]>(this.baseUrl + "/ReportService/ReportSource", { headers: header });
  }

  getReportType(dbName: string): Observable<ReportType[]> {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,
    });

    return this.http.get<ReportType[]>(this.baseUrl + `/ReportService/ReportType/${dbName}`, { headers: header });
  }

  postCreateReport(data: CreateReport): Observable<CreateReport> {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,

    });

    return this.http.post<CreateReport>(this.baseUrl + "/Report/Create", data, { headers: header });
  }

  //GET ALL EDIT DATA
  getReportData(recID: number): Observable<ReportData[]> {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,
    });

    return this.http.get<ReportData[]>(this.baseUrl + `/Report/GetReportDataID?RecId=${recID}`, { headers: header });
  }

  PostReportData(model: PostReportData): Observable<PostReportData> {
    const ss = sessionStorage.getItem('_token');
    const token = JSON.parse(ss).token;

    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + token,
      'Content-Type': 'application/json' // Set the Content-Type header
    });

    return this.http.post<PostReportData>(this.baseUrl + '/Report/PostDataReport', model, { headers: headers });
  }


  getStudyTypes(dbName: string): Observable<StudyData[]> {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,
    });

    return this.http.get<StudyData[]>(this.baseUrl + `/ReportService/StudyDates/${dbName}`, { headers: header });
  }

  getTargetData(dBaseName: string, reportType: string, studywave: string, recID: number): Observable<TargetData[]> {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,
    });

    return this.http.get<TargetData[]>(this.baseUrl + `/ReportService/Targets/${dBaseName}/${reportType}/${studywave}/${recID}`, { headers: header });
  }

  getReportTypeByRecID(recId: number) {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,
    });
    return this.http.get(this.baseUrl + `/ReportService/ReportTypeByRecID/${recId}`, { headers: header });
  }

  getDatabaseName(recId: number) {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,


    });
    return this.http.get(this.baseUrl + `/ReportService/DatabaseName/${recId}`, { headers: header, responseType: 'text' });
  }

  getReportTypeName(typeName: string, RptName: string) {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,

    });
    return this.http.get(this.baseUrl + `/ReportService/ReportTypeName?typeName=${typeName}&RptName=${RptName}`, { headers: header, responseType: 'text' });
  }


  getScalePoints(typeName: string) {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,

    });
    return this.http.get(this.baseUrl + `/ReportService/ScalePoints/${typeName}`, { headers: header });
  }

  getDemographics(typeName: string) {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,

    });
    return this.http.get(this.baseUrl + `/ReportService/Demographics?typeName=${typeName}`, { headers: header });
  }

  UpdateReport(recId: number, updatemodel: UpdateModel) {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,

    });

    return this.http.put(this.baseUrl + `/Report/Update/reports/${recId}`, updatemodel, { headers: header });
  }

  DemoTypeName(typeName: string) {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,

    });
    return this.http.get(this.baseUrl + `/ReportService/DemoTypeName/${typeName}`, { headers: header });
  }

  DemoWithName(typeName: string, DemoName: string) {
    //write your code here.....
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;
    var payload = {
      DatabaseName: typeName,
      DemoTypeName: DemoName
    }

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,

    });
    return this.http.post(this.baseUrl + `/ReportService/DemographicsDemoType`, payload, { headers: header })
  }

  RankingReport(typeName: string) {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,

    });

    return this.http.get(this.baseUrl + `/ReportService/RankingParamter/${typeName}`, { headers: header })
  }

  GetQScoreReportData(typeName: string, recId: number, ReportFormat: number,CategoriesAverage:number) {
    let ss = sessionStorage.getItem('_token');
    let token = JSON.parse(ss).token;

    let header = new HttpHeaders({
      'Authorization': 'Bearer ' + token,

    });
    return this.http.get(this.baseUrl + `/ReportService/QScoreData/${typeName}?RecID=${recId}&ReportFormat=${ReportFormat}&CategoriesAverage=${CategoriesAverage}`, { headers: header });
  }
}
