﻿using System.Collections.Generic;
using PodioAPI.Models;
using System.Threading.Tasks;

namespace PodioAPI.Services
{
    public class QuestionService
    {
        private readonly Podio _podio;

        public QuestionService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Answers the question for the given object. The object type can be either "status" or "comment".
        ///     <para>Podio API Reference: https://developers.podio.com/doc/questions/answer-question-887232 </para>
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="questionOptionId"></param>
        public async Task<dynamic> AnswerQuestion(long
            questionId, long questionOptionId)
        {
            string url = string.Format("/question/{0}/", questionId);
            dynamic requestData = new
            {
                question_option_id = questionOptionId
            };
             return await _podio.Post<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Returns all the answers for the given question on the given object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/questions/get-answers-945753  </para>
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public async Task<List<Answer>> GetAnswers(long questionId)
        {
            string url = string.Format("/question/{0}/", questionId);
            return  await _podio.Get<List<Answer>>(url);
        }

        /// <summary>
        ///     Creates a new question on the given object. Supported object types are "status" and "comment".
        ///     <para>Podio API Reference: https://developers.podio.com/doc/questions/create-question-887166 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="questionText">The text of the question.</param>
        /// <param name="options">The list of text for the option</param>
        /// <returns></returns>
        public async Task<long> CreateQuestion(string refType, long refId, string questionText, List<string> options)
        {
            string url = string.Format("/question/{0}/{1}/", refType, refId);
            dynamic requestData = new
            {
                text = questionText,
                options = options
            };
            dynamic response =  await _podio.Post<dynamic>(url, requestData);
            return (long) response["question_id"];
        }
    }
}