// src/services/ReportService.js
import axios from 'axios';
import { Response } from './Response';
import * as paths from './pathes';


export default function GetReportService() {
  async function sendReportByEmail(storeId, email) {
    return axios
      .post(paths.sendReportByEmailUrl, { storeId, email })
      .then((response) => {
        if (response.data.exceptionHasOccured) {
          alert(response.data.errorMessage);
        }
        return Response.create(response.data.value, response.data.exceptionHasOccured, response.data.errorMessage);
      })
      .catch((error) => {
        console.log('Error sending report by email:', error);
        return Response.create(null, true, error.message);
      });
  }

  async function saveExcelReport(storeId) {
    return axios
      .post(paths.saveExcelReportUrl, { storeId })
      .then((response) => {
        if (response.data.exceptionHasOccured){
          alert(response.data.errorMessage);
        }
        return Response.create(response.data.value, response.data.exceptionHasOccured, response.data.errorMessage);
      })
      .catch((error) => {
        console.log('Error saving Excel report:', error);
        return Response.create(null, true, error.message);
      });
  }

  async function sendReoprtBySms(storeId,phoneNumber) {
    return axios
      .post(paths.saveSmSReportUrl, { storeId,targetNumber:phoneNumber })
      .then((response) => {
        if (response.data.exceptionHasOccured){
          alert(response.data.errorMessage);
        }
        return Response.create(response.data.value, response.data.exceptionHasOccured, response.data.errorMessage);
      })
      .catch((error) => {
        console.log('Error saving Excel report:', error);
        return Response.create(null, true, error.message);
      });
  }


  return { sendReportByEmail, saveExcelReport,sendReoprtBySms};
}
