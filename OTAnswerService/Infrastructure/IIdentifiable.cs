﻿namespace OTAnswerService.Infrastructure
{
    /// <summary>
    /// Used for marking entities which have unique identifier.
    /// </summary>
    public interface IIdentifiable
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        int Id { get; }
    }
}