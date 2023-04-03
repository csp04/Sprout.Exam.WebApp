# Sprout.Exam.WebApp

### If we are going to deploy this on production, what do you think is the next improvement that you will prioritize next? This can be a feature, a tech debt, or an architectural design?

#### Answer
- ##### Feature
  - ##### Search bar
    - to facilitate easy data retrieval for the user.
    - Filter options for advance search.
  - ##### Pagination
    - Assuming we have millions of records, we don't want our API to retrieve records all at once as it will slow down both the client app and server.
  - ##### Actions (Create, Edit, Calculate, Delete)
    - Access to the feature should be restricted to certain roles (such as administrator or super-user).
  - ##### Client validations
    - The UI should handle input validations so we can minimize unnecessary API calls with bad payload.
  - ##### Handle unexpected error
    - Show modal/popup window or show message indicating that something went wrong between the client and the server.
- ##### Tech Debt
  - ##### State management like redux, redux/toolkit
    - Implementing state management can improve development efficiency in the long term by promoting consistency in handling of states across the codebase.
  - ##### React Project Structure
    - ##### Any can be useful
      - ##### a.) Group similar files
        - components/ui (reusable components)
        - slices (state management + webapp logics)
        - utils/helpers (helper functions)
        - pages
        - apis (direct api calls)
        - contexts (react context)
      - ##### b.) Group by feature
        - features (contains the api, child component, page, state management(slice), and other related for this feature)
        - components/ui (reusable components)
        - utils/helpers (helper functions)
